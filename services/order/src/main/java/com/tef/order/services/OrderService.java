package com.tef.order.services;

import com.tef.order.dtos.ItemAdditionParametersDto;
import com.tef.order.dtos.ItemDto;
import com.tef.order.dtos.ItemUpdateCountDto;
import com.tef.order.dtos.OrderDto;
import com.tef.order.models.OrderModel;
import com.tef.order.models.OrderItem;
import com.tef.order.repositories.OrderItemRepository;
import com.tef.order.repositories.OrderRepository;
import com.tef.order.types.OrderItemId;
import com.tef.order.types.OrderStatus;
import org.springframework.amqp.core.AmqpTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

import static com.tef.order.types.OrderStatus.*;

@Service
public class OrderService {
    private final OrderRepository orderRepository;
    private final OrderItemRepository orderItemRepository;
    private final String wareHouseUrl = "http://localhost:8181/api/warehouse/";

    @Autowired
    AmqpTemplate template;

    public OrderService(OrderRepository orderRepository, OrderItemRepository orderItemRepository) throws Exception {
        this.orderRepository = orderRepository;
        this.orderItemRepository = orderItemRepository;
    }

    //TODO: add method for removing order on cancel or failing
    public List<OrderDto> getOrders() {
        return StreamSupport
                .stream(orderRepository
                        .findAll()
                        .spliterator(), false)
                .map(m -> {
                    try {
                        return getOrderById(m.getId());
                    } catch (Exception e) {
                        e.printStackTrace();
                        return null;
                    }
                })
                .collect(Collectors.toList());
    }

    public OrderDto getOrderById(Integer id) throws Exception {
        Optional<OrderModel> order = orderRepository.findById(id);
        if (order.isEmpty())
            throw new Exception("order not found: " + id);

        var orderDto = OrderDto.fromOrder(order.get());
        var items = StreamSupport
                .stream(orderItemRepository
                        .findAll()
                        .spliterator(), false)
                .filter(i -> i.getOrderId() == id)
                .map(ItemDto::CreateFrom)
                .collect(Collectors.toList());

        Double totalPrice = 0.0;
        int totalAmount = 0;
        for (int i = 0; i < items.size(); i++) {
            totalAmount += items.get(i).getAmount();
            totalPrice += items.get(i).getPrice() * items.get(i).getAmount();
        }
        orderDto.setItems(items);
        orderDto.setTotalAmount(totalAmount);
        orderDto.setTotalCost(totalPrice);
        return orderDto;
    }

    public Integer addItemToOrder(Optional<Integer> orderId,
                                  Integer itemId,
                                  ItemAdditionParametersDto addInfo) throws Exception {
        OrderModel orderModel;

        if (orderId.isEmpty()) {
            orderModel = new OrderModel();
            orderModel.setOrderStatus(Collecting);
            orderModel = orderRepository.save(orderModel);
        }
        else {
            Optional<OrderModel> orderInDb = orderRepository.findById(orderId.get());
            if (orderInDb.isEmpty())
                throw new Exception("order not found: " + orderId);
            orderModel = orderInDb.get();
        }

        String getItemUrl = wareHouseUrl + "/items/" + itemId.toString();
        ItemDto item =  new RestTemplate().getForObject(getItemUrl, ItemDto.class);
        //TODO: check if item exist - inc amount
        OrderItem orderItem = OrderItem.CreateFrom(item);
        orderItem.setOrderId(orderModel.getId());
        orderItem.setAmount(addInfo.getAmount());
        orderItemRepository.save(orderItem);

        ItemUpdateCountDto itemUpdate = new ItemUpdateCountDto();
        itemUpdate.setId(itemId);
        itemUpdate.setAmount(addInfo.getAmount());
        template.convertAndSend("item-remove", itemUpdate);

        return orderModel.getId();
    }

    public void removeItemFromOrder(Integer orderId, Integer itemId) {
        OrderItemId orderItemId = new OrderItemId(orderId, itemId);
        orderItemRepository.deleteById(orderItemId);

        ItemUpdateCountDto itemUpdate = new ItemUpdateCountDto();
        itemUpdate.setId(itemId);
        itemUpdate.setAmount(1);
        template.convertAndSend("item-add", itemUpdate);
    }

    public void changeOrderStatus(Integer id, OrderStatus status) throws Exception {
        if (status == Failed || status == Canceled) {
            OrderDto order = getOrderById(id);
            var items = order.getItems();
            for (int i = 0; i < items.size(); i++) {
                removeItemFromOrder(id, items.get(i).getId());
            }
        }

        Optional<OrderModel> orderModel = orderRepository.findById(id);
        OrderModel instance = orderModel.get();
        instance.setOrderStatus(status);
        orderRepository.save(instance);
    }
}

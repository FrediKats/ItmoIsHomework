package com.tef.order.services;

import com.tef.order.dtos.ItemDto;
import com.tef.order.dtos.OrderDto;
import com.tef.order.models.OrderModel;
import com.tef.order.models.OrderItem;
import com.tef.order.repositories.OrderItemRepository;
import com.tef.order.repositories.OrderRepository;
import com.tef.order.types.OrderItemId;
import com.tef.order.types.OrderStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

import static com.tef.order.types.OrderStatus.Collecting;

@Service
public class OrderService {
    private final OrderRepository orderRepository;
    private final OrderItemRepository orderItemRepository;
    private final String wareHouseUrl = "http://localhost:8181/api/warehouse/";

    public OrderService(OrderRepository orderRepository, OrderItemRepository orderItemRepository) {
        this.orderRepository = orderRepository;
        this.orderItemRepository = orderItemRepository;
    }

    //TODO: add method for removing order on cancel or failing
    public List<OrderDto> getOrders() {
        // TODO: count
        return StreamSupport
                .stream(orderRepository
                        .findAll()
                        .spliterator(), false)
                .map(OrderDto::fromOrder)
                .collect(Collectors.toList());
    }

    public OrderDto getOrderById(Integer id) throws Exception {
        Optional<OrderModel> order = orderRepository.findById(id);
        if (order.isEmpty())
            throw new Exception("order not found: " + id);

        return OrderDto.fromOrder(order.get());
    }

    public Integer addItemToOrder(Optional<Integer> orderId, Integer itemId) throws Exception {
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
        //TODO: add to warehouse method for removing
        //TODO: remove item from warehouse service
        OrderItem orderItem = OrderItem.CreateFrom(item);
        orderItem.setOrderId(orderModel.getId());
        orderItemRepository.save(orderItem);

        return orderModel.getId();
    }

    public void removeItemFromOrder(Integer orderId, Integer itemId) {
        OrderItemId orderItemId = new OrderItemId(orderId, itemId);
        orderItemRepository.deleteById(orderItemId);
    }

    public void changeOrderStatus(Integer id, OrderStatus status) throws Exception {
        Optional<OrderModel> order = orderRepository.findById(id);
        if (order.isEmpty())
            throw new Exception("order not found: " + id);

        OrderModel instance = order.get();
        instance.setOrderStatus(status);
        orderRepository.save(instance);
    }
}

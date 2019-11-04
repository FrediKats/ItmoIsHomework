package com.tef.order.services;

import com.tef.order.dtos.OrderDto;
import com.tef.order.models.Order;
import com.tef.order.models.OrderItem;
import com.tef.order.repositories.OrderItemRepository;
import com.tef.order.repositories.OrderRepository;
import com.tef.order.types.OrderItemId;
import com.tef.order.types.OrderStatus;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@Service
public class OrderService {
    private Logger logger = LoggerFactory.getLogger(OrderService.class);

    @Autowired
    private OrderRepository orderRepository;
    @Autowired
    private OrderItemRepository orderItemRepository;

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
        Optional<Order> order = orderRepository.findById(id);
        if (order.isEmpty())
            throw new Exception("order not found: " + id);

        return OrderDto.fromOrder(order.get());
    }

    public void addItemToOrder(Optional<Integer> orderId, Integer itemId) throws Exception {
        Order order;

        if (orderId.isEmpty()) {
            order = new Order();
            //TODO: add smth?
            order = orderRepository.save(order);
        }
        else {
            Optional<Order> orderInDb = orderRepository.findById(orderId.get());
            if (orderInDb.isEmpty())
                throw new Exception("order not found: " + orderId);
            order = orderInDb.get();
        }

        OrderItem orderItem = new OrderItem();
        orderItem.setOrderId(order.getId());
        orderItem.setItemId(itemId);
        //TODO: check if item exist - inc amount
        orderItem.setAmount(1);

        //TODO: get item from other service and save info here

        orderItemRepository.save(orderItem);
    }

    public void removeItemFromOrder(Integer orderId, Integer itemId) {
        OrderItemId orderItemId = new OrderItemId(orderId, itemId);
        orderItemRepository.deleteById(orderItemId);
    }

    public void changeOrderStatus(Integer id, OrderStatus status) throws Exception {
        Optional<Order> order = orderRepository.findById(id);
        if (order.isEmpty())
            throw new Exception("order not found: " + id);

        Order instance = order.get();
        instance.setOrderStatus(status);
        orderRepository.save(instance);
    }
}

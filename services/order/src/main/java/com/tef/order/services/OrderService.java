package com.tef.order.services;

import com.tef.order.dtos.OrderDto;
import com.tef.order.models.Order;
import com.tef.order.models.OrderItem;
import com.tef.order.repositories.OrderItemRepository;
import com.tef.order.repositories.OrderRepository;
import com.tef.order.types.OrderItemId;
import com.tef.order.types.OrderStatus;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.PathVariable;

import java.lang.reflect.Array;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@Service
public class OrderService {
    @Autowired
    private OrderRepository orderRepository;
    private OrderItemRepository orderItemRepository;

    public List<OrderDto> getOrders() {
        // TODO: count
        return StreamSupport
                .stream(orderRepository
                        .findAll()
                        .spliterator(), false)
                .map(OrderDto::fromOrder)
                .map(orderDto -> {

                    return orderDto;
                })
                .collect(Collectors.toList());
    }

    public OrderDto getOrderById(Integer id) throws Exception {
        Optional<Order> order = orderRepository.findById(id);
        if (order.isEmpty())
            throw new Exception("order not found: " + id);

        return OrderDto.fromOrder(order.get());
    }

    public void addItemToOrder(Integer orderId, Integer itemId) throws Exception {
        Optional<Order> order = orderRepository.findById(orderId);
        OrderItem orderItem = new OrderItem();
        orderItem.setOrderId(orderId);
        orderItem.setItemId(itemId);

        if (order.isEmpty())
            throw new Exception("order not found: " + orderId);


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
        instance.setStatus(status);
        orderRepository.save(instance);
    }
}

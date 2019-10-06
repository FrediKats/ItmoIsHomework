package com.tef.order.services;

import com.tef.order.dtos.ItemDto;
import com.tef.order.dtos.OrderDto;
import com.tef.order.repositories.OrderRepository;
import com.tef.order.types.OrderStatus;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PathVariable;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class OrderService {
    @Autowired
    private OrderRepository orderRepository;

    public List<OrderDto> getOrders() {
    }

    public void getOrderById(Integer id) {
    }

    public void addItemToOrder(Integer orderId, Integer itemId) {
    }

    public ItemDto removeItemFromOrder(Integer orderId) {
    }

    public void changeOrderStatus(Integer order_id, OrderStatus status) {
    }
}

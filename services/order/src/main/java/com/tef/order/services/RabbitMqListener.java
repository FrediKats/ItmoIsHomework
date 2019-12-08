package com.tef.order.services;

import com.tef.order.dtos.OrderStatusUpdateMessage;
import org.springframework.amqp.rabbit.annotation.EnableRabbit;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.Payload;
import org.springframework.stereotype.Component;

@EnableRabbit
@Component
public class RabbitMqListener {
    @Autowired
    private OrderService orderService;

    @RabbitListener(queues = "order-status-update")
    public void OrderStatusUpdateProcess(@Payload OrderStatusUpdateMessage msg) throws Exception {
        System.out.println(msg.getNewStatus() + ": " + msg.getOrderId());
        orderService.changeOrderStatus(msg.getOrderId(), msg.getNewStatus());
    }
}

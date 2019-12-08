package com.tef.order.services;

import com.tef.order.dtos.OrderStatusUpdateMessage;
import org.springframework.amqp.rabbit.annotation.EnableRabbit;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.logging.Logger;

@EnableRabbit
@Component
public class RabbitMqListener {
    Logger logger = Logger.getLogger(RabbitMqListener.class.getName());

    @Autowired
    private OrderService orderService;

    @RabbitListener(queues = "order-status-update")
    public void OrderStatusUpdateProcess(OrderStatusUpdateMessage msg) throws Exception {
        logger.info("Received from queue 1: " + msg);
        orderService.changeOrderStatus(msg.getOrderId(), msg.getNewStatus());
    }
}

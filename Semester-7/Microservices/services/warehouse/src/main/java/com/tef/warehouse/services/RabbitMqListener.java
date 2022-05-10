package com.tef.warehouse.services;

import com.tef.warehouse.dto.ItemUpdateCountDto;
import org.springframework.amqp.rabbit.annotation.EnableRabbit;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.Payload;
import org.springframework.stereotype.Component;

@EnableRabbit
@Component
public class RabbitMqListener {
    @Autowired
    private WarehouseService warehouseService;

    @RabbitListener(queues = "item-add")
    public void ItemAddHandler(@Payload ItemUpdateCountDto itemUpdate) throws Exception {
        warehouseService.addProduct(itemUpdate.getId(), itemUpdate.getAmount());
    }

    @RabbitListener(queues = "item-remove")
    public void ItemRemoveHandler(@Payload ItemUpdateCountDto itemUpdate) throws Exception {
        warehouseService.addProduct(itemUpdate.getId(), itemUpdate.getAmount() * -1);
    }
}
package com.tef.order.models;

import com.tef.order.dtos.ItemDto;
import com.tef.order.types.OrderItemId;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.IdClass;

@Entity
@IdClass(OrderItemId.class)
public class OrderItem {
    @Id
    private Integer orderId;
    @Id
    private Integer itemId;
    private Integer amount;
    private String name;
    private Double price;

    public Integer getOrderId() {
        return orderId;
    }

    public void setOrderId(Integer orderId) {
        this.orderId = orderId;
    }

    public Integer getItemId() {
        return itemId;
    }

    public void setItemId(Integer itemId) {
        this.itemId = itemId;
    }

    public Integer getAmount() {
        return amount;
    }

    public void setAmount(Integer amount) {
        this.amount = amount;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Double getPrice() {
        return price;
    }

    public void setPrice(Double price) {
        this.price = price;
    }

    public static OrderItem CreateFrom(ItemDto item) {
        OrderItem orderItem = new OrderItem();
        orderItem.setItemId(item.getId());
        orderItem.setName(item.getName());
        orderItem.setPrice(item.getPrice());
        return orderItem;
    }
}

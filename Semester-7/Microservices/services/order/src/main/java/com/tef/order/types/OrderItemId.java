package com.tef.order.types;

import java.io.Serializable;

// https://www.baeldung.com/jpa-composite-primary-keys
public class OrderItemId implements Serializable {
    private Integer orderId;
    private Integer itemId;

    public OrderItemId() {
    }

    public OrderItemId(Integer orderId, Integer itemId) {
        this.orderId = orderId;
        this.itemId = itemId;
    }

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
}

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
}

package com.tef.order.types;

import java.io.Serializable;

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

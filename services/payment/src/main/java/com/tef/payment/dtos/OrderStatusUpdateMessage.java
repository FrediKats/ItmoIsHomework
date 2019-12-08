package com.tef.payment.dtos;

import com.tef.payment.types.OrderStatus;

public class OrderStatusUpdateMessage {
    private Integer orderId;
    private OrderStatus newStatus;

    public Integer getOrderId() {
        return orderId;
    }

    public void setOrderId(Integer orderId) {
        this.orderId = orderId;
    }

    public OrderStatus getNewStatus() {
        return newStatus;
    }

    public void setNewStatus(OrderStatus newStatus) {
        this.newStatus = newStatus;
    }
}

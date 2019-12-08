package com.tef.order.dtos;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.tef.order.types.OrderStatus;
import lombok.NoArgsConstructor;

@NoArgsConstructor
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

    public OrderStatusUpdateMessage(
            @JsonProperty("orderId") Integer orderId,
            @JsonProperty("newStatus") OrderStatus newStatus) {
        this.orderId = orderId;
        this.newStatus = newStatus;
    }
}
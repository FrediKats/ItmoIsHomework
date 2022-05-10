package com.tef.payment.models;


import com.tef.payment.types.OrderStatus;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
public class OrderInfo {
    @Id
    private Integer orderId;
    private String address;
    private String username;
    private OrderStatus orderStatus;

    public Integer getOrderId() {
        return orderId;
    }

    public OrderInfo setOrderId(Integer orderId) {
        this.orderId = orderId;
        return this;
    }

    public String getAddress() {
        return address;
    }

    public OrderInfo setAddress(String address) {
        this.address = address;
        return this;
    }

    public String getUsername() {
        return username;
    }

    public OrderInfo setUsername(String username) {
        this.username = username;
        return this;
    }

    public OrderStatus getOrderStatus() {
        return orderStatus;
    }

    public void setOrderStatus(OrderStatus orderStatus) {
        this.orderStatus = orderStatus;
    }
}

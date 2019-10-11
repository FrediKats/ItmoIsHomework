package com.tef.payment.models;


import com.tef.payment.types.OrderStatus;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import java.util.Date;

@Entity
public class OrderStatusLog {
    @Id
    @GeneratedValue(strategy= GenerationType.AUTO)
    private Integer orderId;
    private Date date;
    private OrderStatus prevStatus;
    private OrderStatus newStatus;

    public Integer getOrderId() {
        return orderId;
    }

    public OrderStatusLog setOrderId(Integer orderId) {
        this.orderId = orderId;
        return this;
    }

    public Date getDate() {
        return date;
    }

    public OrderStatusLog setDate(Date date) {
        this.date = date;
        return this;
    }

    public OrderStatus getPrevStatus() {
        return prevStatus;
    }

    public OrderStatusLog setPrevStatus(OrderStatus prevStatus) {
        this.prevStatus = prevStatus;
        return this;
    }

    public OrderStatus getNewStatus() {
        return newStatus;
    }

    public OrderStatusLog setNewStatus(OrderStatus newStatus) {
        this.newStatus = newStatus;
        return this;
    }

}

package com.tef.order.dtos;

import com.tef.order.models.OrderModel;
import com.tef.order.types.OrderStatus;

import java.util.ArrayList;
import java.util.List;

public class OrderDto {
    private Integer id;
    private OrderStatus status;
    private Double totalCost;
    private Integer totalAmount;
    private List<ItemDto> items;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public OrderStatus getStatus() {
        return status;
    }

    public void setStatus(OrderStatus status) {
        this.status = status;
    }

    public Double getTotalCost() {
        return totalCost;
    }

    public void setTotalCost(Double totalCost) {
        this.totalCost = totalCost;
    }

    public Integer getTotalAmount() {
        return totalAmount;
    }

    public void setTotalAmount(Integer totalAmount) {
        this.totalAmount = totalAmount;
    }

    public List<ItemDto> getItems() {
        return items;
    }

    public void setItems(List<ItemDto> items) {
        this.items = items;
    }

    public static OrderDto fromOrder(OrderModel orderModel) {
        OrderDto dto = new OrderDto();
        dto.setId(orderModel.getId());
        dto.setStatus(orderModel.getOrderStatus());
        return dto;
    }
}

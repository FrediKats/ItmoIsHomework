package com.tef.payment.dtos;
import com.tef.payment.types.OrderStatus;

import java.util.ArrayList;

public class OrderDto {
  private Integer id;
  private OrderStatus status;
  private Double totalCost;
  private Integer totalAmount;
  private String username;
  private ArrayList<ItemDto> items;

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

  public String getUsername() {
    return username;
  }

  public void setUsername(String username) {
    this.username = username;
  }

  public ArrayList<ItemDto> getItems() {
    return items;
  }

  public void setItems(ArrayList<ItemDto> items) {
    this.items = items;
  }
}

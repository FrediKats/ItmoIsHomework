package com.tef.order.dtos;

import com.tef.order.models.OrderItem;

public class ItemDto {
    private Integer _id;
    private String _name;
    private Integer _amount;
    private Double price;

    public Integer getId() {
        return _id;
    }

    public void setId(Integer id) {
        _id = id;
    }

    public String getName() {
        return _name;
    }

    public void setName(String name) {
        _name = name;
    }

    public Integer getAmount() {
        return _amount;
    }

    public void setAmount(Integer amount) {
        _amount = amount;
    }

    public Double getPrice() {
        return price;
    }

    public void setPrice(Double price) {
        this.price = price;
    }

    public static ItemDto CreateFrom(OrderItem item)
    {
        ItemDto dto = new ItemDto();
        dto.setAmount(item.getAmount());
        dto.setId(item.getItemId());
        dto.setName(item.getName());
        dto.setPrice(item.getPrice());

        return dto;
    }
}
package com.tef.warehouse.dto;

import com.tef.warehouse.models.Item;

public class ItemDto {
    private Integer _id;
    private String _name;
    private Integer _amount;
    private Double price;

    public static ItemDto fromItem(Item item) {
        ItemDto dto = new ItemDto();
        dto.setId(item.getId());
        dto.setName(item.getName());
        dto.setAmount(item.getAmount());
        dto.setPrice(item.getPrice());
        return dto;
    }

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
}

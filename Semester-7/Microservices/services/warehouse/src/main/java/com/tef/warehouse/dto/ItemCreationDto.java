package com.tef.warehouse.dto;

import com.tef.warehouse.models.Item;

public class ItemCreationDto {
    private String _name;
    private Integer _amount;
    private Double _price;

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
        return _price;
    }

    public void setPrice(Double price) {
        _price = price;
    }

    public Item toItem() {
        Item item = new Item();
        item.setAmount(getAmount());
        item.setName(getName());
        item.setPrice(getPrice());
        return item;
    }
}

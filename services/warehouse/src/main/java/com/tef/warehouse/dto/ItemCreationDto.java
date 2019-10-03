package com.tef.warehouse.dto;

import org.joda.money.Money;

public class ItemCreationDto {
    private String _name;
    private Integer _amount;
    private Money _price;

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

    public Money getPrice() {
        return _price;
    }

    public void setPrice(Money price) {
        _price = price;
    }
}

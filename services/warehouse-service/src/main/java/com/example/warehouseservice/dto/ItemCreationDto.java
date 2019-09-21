package com.example.warehouseservice.dto;

public class ItemCreationDto {
    private String _name;
    private Integer _amount;

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
}

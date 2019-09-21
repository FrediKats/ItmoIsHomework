package com.example.warehouseservice.dto;

public class ItemDto {
    private Integer _id;
    private String _name;
    private Integer _amount;

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
}

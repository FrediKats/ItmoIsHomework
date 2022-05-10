package com.tef.warehouse.dto;

public class ItemUpdateCountDto {
    private Integer _id;
    private Integer _amount;

    public Integer getId() {
        return _id;
    }

    public void setId(Integer id) {
        _id = id;
    }

    public Integer getAmount() {
        return _amount;
    }

    public void setAmount(Integer amount) {
        _amount = amount;
    }
}

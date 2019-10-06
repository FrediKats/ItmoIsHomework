package com.tef.payment.dtos;

public class PaymentInfoDto {
    private String _userName;
    private Integer _orderId;
    private Integer _amount;

    public String getUserName() {
        return _userName;
    }

    public void setUserName(String userName) {
        _userName = userName;
    }

    public Integer getOrderId() {
        return _orderId;
    }

    public void setOrderId(Integer orderId) {
        _orderId = orderId;
    }

    public Integer getAmount() {
        return _amount;
    }

    public void setAmount(Integer amount) {
        _amount = amount;
    }
}
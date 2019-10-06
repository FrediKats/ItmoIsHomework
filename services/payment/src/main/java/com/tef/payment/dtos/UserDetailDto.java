package com.tef.payment.dtos;

enum CardAuthorizationInfo {
    authorized,
    unauthorized
}

public class UserDetailDto {
    private String _userName;
    private CardAuthorizationInfo _cardAuthorizationInfo;

    public String getUserName() {
        return _userName;
    }

    public void setUserName(String userName) {
        _userName = userName;
    }

    public CardAuthorizationInfo getCardAuthorizationInfo() {
        return _cardAuthorizationInfo;
    }

    public void setCardAuthorizationInfo(CardAuthorizationInfo cardAuthorizationInfo) {
        _cardAuthorizationInfo = cardAuthorizationInfo;
    }

}
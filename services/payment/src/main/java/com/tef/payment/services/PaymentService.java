package com.tef.payment.services;

import com.tef.payment.dtos.PaymentInfoDto;
import com.tef.payment.repositories.OrderInfoRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class PaymentService {
    @Autowired
    private OrderInfoRepository orderInfoRepository;

    public PaymentInfoDto performPayment(Integer orderId) {
    }

    public PaymentInfoDto addPaymentInfo(Integer orderId) {
    }

    public PaymentInfoDto cancelPayment(Integer orderId) {
    }
}

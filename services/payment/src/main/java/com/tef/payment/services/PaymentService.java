package com.tef.payment.services;

import com.tef.payment.dtos.PaymentInfoDto;
import com.tef.payment.repositories.PaymentRepository;
import org.springframework.beans.factory.annotation.Autowired;

public class PaymentService {
    @Autowired
    private PaymentRepository paymentRepository;

    public PaymentInfoDto performPayment(Integer orderId) {
    }

    public PaymentInfoDto addPaymentInfo(Integer orderId) {
    }

    public PaymentInfoDto cancelPayment(Integer orderId) {
    }
}

package com.tef.payment;

import com.tef.payment.dtos.PaymentInfoDto;
import com.tef.payment.dtos.UserDetailDto;
import com.tef.payment.services.PaymentService;
import com.tef.payment.types.OrderStatus;
import org.springframework.web.bind.annotation.*;

@RestController
public class PaymentController {
    private final PaymentService paymentService;

    public PaymentController(PaymentService paymentService) {
        this.paymentService = paymentService;
    }

    //TODO: fix null
    @PostMapping("api/payment/{orderId}")
    public OrderStatus performPayment(@PathVariable Integer orderId, @RequestBody UserDetailDto userDetailDto) throws Exception {
        return paymentService.performPayment(orderId, userDetailDto);
    }

    @PostMapping("api/payment/{orderId}/cancel")
    public OrderStatus cancelPayment(@PathVariable Integer orderId) throws Exception {
        return paymentService.cancelPayment(orderId);
    }
}

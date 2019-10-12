package com.tef.payment;

import com.tef.payment.dtos.PaymentInfoDto;
import com.tef.payment.services.PaymentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
public class PaymentController {
    @Autowired
    private PaymentService paymentService;

    public PaymentController() {
        paymentService = new PaymentService();
    }

    @PostMapping("api/payment/{order_id}")
    public PaymentInfoDto performPayment(@PathVariable Integer order_id) throws Exception {
        return paymentService.performPayment(order_id);
    }

    @PostMapping("api/payment/{order_id}/info")
    public PaymentInfoDto addPaymentInfo(@PathVariable Integer order_id) {
        return paymentService.addPaymentInfo(order_id);
    }

    @PostMapping("api/payment/{order_id}/cancel")
    public PaymentInfoDto cancelPayment(@PathVariable Integer order_id) {
        return paymentService.cancelPayment(order_id);
    }
}

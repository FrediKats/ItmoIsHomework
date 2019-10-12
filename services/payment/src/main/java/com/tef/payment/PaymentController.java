package com.tef.payment;

import com.tef.payment.dtos.OrderDto;
import com.tef.payment.dtos.PaymentInfoDto;
import com.tef.payment.dtos.UserDetailDto;
import com.tef.payment.services.PaymentService;
import org.springframework.data.domain.jaxb.SpringDataJaxb;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
public class PaymentController {
    @Autowired
    private PaymentService paymentService;

    public PaymentController() {
        paymentService = new PaymentService();
    }

    @PostMapping("api/payment/{orderId}")
    public PaymentInfoDto performPayment(@PathVariable Integer orderId, @RequestBody UserDetailDto userDetailDto) throws Exception {
        paymentService.performPayment(orderId, userDetailDto);
        return null;
    }

    @PostMapping("api/payment/add-info")
    public PaymentInfoDto addPaymentInfo(@RequestBody PaymentInfoDto orderDto) throws Exception {
        paymentService.addPaymentInfo(orderDto);
        return null;
    }

    @PostMapping("api/payment/{orderId}/cancel")
    public PaymentInfoDto cancelPayment(@PathVariable Integer orderId) throws Exception {
        paymentService.cancelPayment(orderId);
        return null;
    }
}

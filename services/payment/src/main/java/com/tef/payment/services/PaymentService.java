package com.tef.payment.services;

import com.tef.payment.dtos.PaymentInfoDto;
import com.tef.payment.dtos.UserDetailDto;
import com.tef.payment.models.OrderInfo;
import com.tef.payment.repositories.OrderInfoRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.tef.payment.types.CardAuthorizationInfo;
import com.tef.payment.types.OrderStatus;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class PaymentService {
    private final OrderInfoRepository orderInfoRepository;

    public PaymentService(OrderInfoRepository orderInfoRepository) {
        this.orderInfoRepository = orderInfoRepository;
    }

    //TODO: return order state
    public void performPayment(Integer orderId, UserDetailDto userDetailDto) throws Exception {
        Optional<OrderInfo> orderInfo = orderInfoRepository.findById(orderId);
        if (!orderInfo.isPresent())
            throw new Exception("order not found: " + orderId);

        OrderInfo instance = orderInfo.get();
        if (userDetailDto.getCardAuthorizationInfo() == CardAuthorizationInfo.AUTHORIZED)
            instance.setOrderStatus(OrderStatus.Payed);
        else
            //TODO: remove order from order service
            instance.setOrderStatus(OrderStatus.Failed);

        orderInfoRepository.save(instance);

    }

    //TODO: разобраться с OrderDto
    //TODO: return order state
    public void addPaymentInfo(PaymentInfoDto paymentInfoDto) throws Exception {
        OrderInfo orderInfo = new OrderInfo();
        orderInfo.setUsername(paymentInfoDto.getUserName());
        orderInfo.setOrderId(paymentInfoDto.getOrderId());
        orderInfo.setOrderStatus(OrderStatus.Collecting);
        orderInfoRepository.save(orderInfo);
    }

    //TODO: return order state
    public void cancelPayment(Integer orderId) throws Exception {
        Optional<OrderInfo> orderInfo = orderInfoRepository.findById(orderId);
        if (!orderInfo.isPresent())
            throw new Exception("order not found: " + orderId);

        OrderInfo instance = orderInfo.get();
        instance.setOrderStatus(OrderStatus.Canceled);
        orderInfoRepository.save(instance);
        //TODO: remove order from order service
    }
}

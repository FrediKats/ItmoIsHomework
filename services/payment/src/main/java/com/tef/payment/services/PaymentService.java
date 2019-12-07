package com.tef.payment.services;

import com.tef.payment.dtos.PaymentInfoDto;
import com.tef.payment.dtos.UserDetailDto;
import com.tef.payment.models.OrderInfo;
import com.tef.payment.repositories.OrderInfoRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cloud.stream.annotation.EnableBinding;
import org.springframework.cloud.stream.messaging.Source;
import org.springframework.messaging.support.MessageBuilder;
import org.springframework.stereotype.Service;

import com.tef.payment.types.CardAuthorizationInfo;
import com.tef.payment.types.OrderStatus;

import java.util.Optional;

@EnableBinding(Source.class)
@Service
public class PaymentService {
    private final OrderInfoRepository orderInfoRepository;
    private final String orderServiceUrl = "http://localhost:8182/api/orders/";

    @Autowired
    private Source mysource;

    public PaymentService(OrderInfoRepository orderInfoRepository) {
        this.orderInfoRepository = orderInfoRepository;
    }

    public OrderStatus performPayment(Integer orderId, UserDetailDto userDetailDto) throws Exception {
        Optional<OrderInfo> orderInfo = orderInfoRepository.findById(orderId);
        if (!orderInfo.isPresent())
            throw new Exception("order not found: " + orderId);

        OrderInfo instance = orderInfo.get();
        if (userDetailDto.getCardAuthorizationInfo() == CardAuthorizationInfo.AUTHORIZED)
            instance.setOrderStatus(OrderStatus.Payed);
        else
            instance.setOrderStatus(OrderStatus.Failed);

        updateStateRemote(orderId, instance.getOrderStatus());
        orderInfoRepository.save(instance);
        return instance.getOrderStatus();
    }

    //TODO: разобраться с OrderDto
    public OrderStatus addPaymentInfo(PaymentInfoDto paymentInfoDto) throws Exception {
        OrderInfo orderInfo = new OrderInfo();
        orderInfo.setUsername(paymentInfoDto.getUserName());
        orderInfo.setOrderId(paymentInfoDto.getOrderId());
        orderInfo.setOrderStatus(OrderStatus.Collecting);

        updateStateRemote(orderInfo.getOrderId(), orderInfo.getOrderStatus());
        orderInfoRepository.save(orderInfo);
        return orderInfo.getOrderStatus();
    }

    public OrderStatus cancelPayment(Integer orderId) throws Exception {
        Optional<OrderInfo> orderInfo = orderInfoRepository.findById(orderId);
        if (!orderInfo.isPresent())
            throw new Exception("order not found: " + orderId);

        OrderInfo instance = orderInfo.get();
        instance.setOrderStatus(OrderStatus.Canceled);
        updateStateRemote(orderId, instance.getOrderStatus());
        orderInfoRepository.save(instance);
        //TODO: remove order from order service
        return instance.getOrderStatus();
    }

    private void updateStateRemote(Integer orderId, OrderStatus status) {
        String getItemUrl = orderServiceUrl + orderId.toString() + "/status/" + status.toString();

        mysource.output().send(MessageBuilder.withPayload(status.toString()).build());
    }
}

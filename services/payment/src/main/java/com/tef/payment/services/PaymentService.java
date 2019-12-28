package com.tef.payment.services;

import com.tef.payment.dtos.*;
import com.tef.payment.repositories.OrderInfoRepository;
import org.springframework.amqp.core.AmqpTemplate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cloud.stream.annotation.EnableBinding;
import org.springframework.cloud.stream.messaging.Source;
import org.springframework.stereotype.Service;

import com.tef.payment.types.CardAuthorizationInfo;
import com.tef.payment.types.OrderStatus;
import org.springframework.web.client.RestTemplate;


@EnableBinding(Source.class)
@Service
public class PaymentService {
    private final String orderServiceUrl = "http://localhost:8182/api/orders/";

    @Autowired
    AmqpTemplate template;

    public OrderStatus performPayment(Integer orderId, UserDetailDto userDetailDto) throws Exception {
        var order = getOrderFromService(orderId);
        if (order == null)
            throw new Exception("order not found: " + orderId);

        OrderStatus instance;
        if (userDetailDto.getCardAuthorizationInfo() == CardAuthorizationInfo.AUTHORIZED)
            instance = (OrderStatus.Payed);
        else
            instance = (OrderStatus.Failed);

        OrderStatusUpdateMessage orderStatusUpdateMessage = new OrderStatusUpdateMessage();
        orderStatusUpdateMessage.setNewStatus(instance);
        orderStatusUpdateMessage.setOrderId(orderId);
        updateStateRemote(orderStatusUpdateMessage);
        //orderInfoRepository.save(instance);
        return instance;
    }

    public OrderStatus cancelPayment(Integer orderId) throws Exception {
        var order = getOrderFromService(orderId);
        if (order == null)
            throw new Exception("order not found: " + orderId);

        OrderStatusUpdateMessage orderStatusUpdateMessage = new OrderStatusUpdateMessage();
        orderStatusUpdateMessage.setNewStatus(OrderStatus.Canceled);
        orderStatusUpdateMessage.setOrderId(orderId);
        updateStateRemote(orderStatusUpdateMessage);
        return OrderStatus.Canceled;
    }

    private void updateStateRemote(OrderStatusUpdateMessage orderStatusUpdateInfo) {
        template.convertAndSend("order-status-update", orderStatusUpdateInfo);
    }

    private OrderDto getOrderFromService(Integer id) {
        String getItemUrl = orderServiceUrl + id.toString();
        OrderDto order =  new RestTemplate().getForObject(getItemUrl, OrderDto.class);
        return order;
    }
}

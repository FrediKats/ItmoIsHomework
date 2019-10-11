package com.tef.payment.services;

import com.tef.payment.dtos.OrderDto;
import com.tef.payment.dtos.PaymentInfoDto;
import com.tef.payment.dtos.UserDetailDto;
import com.tef.payment.models.OrderInfo;
import com.tef.payment.models.OrderStatusLog;
import com.tef.payment.repositories.OrderInfoRepository;
import com.tef.payment.repositories.OrderStatusLogRepository;
import com.tef.payment.types.OrderStatus;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.Optional;

public class PaymentService {
  @Autowired
  private OrderInfoRepository orderInfoRepository;
  private OrderStatusLogRepository orderStatusLogRepository;

  //TODO: изменить с void
  void performPayment(Integer orderId, UserDetailDto userDetailDto) throws Exception {
    Optional<OrderInfo> orderInfo = orderInfoRepository.findById(orderId);
    if (!orderInfo.isPresent())
      throw new Exception("order not found: " + orderId);

    OrderInfo instance = orderInfo.get();
    instance.setUsername(userDetailDto.getUserName());

    Optional<OrderStatusLog> orderStatusLog = orderStatusLogRepository.findById(orderId);
    if (!orderStatusLog.isPresent())
      throw new Exception("order not found: " + orderId);

    OrderStatusLog instanceLog = orderStatusLog.get();
    instanceLog.setNewStatus(OrderStatus.Payed);

    orderInfoRepository.save(instance);
    orderStatusLogRepository.save(instanceLog);

  }

  //TODO: разобраться с OrderDto
  //TODO: изменить с void
  void addPaymentInfo(PaymentInfoDto paymentInfoDto) throws Exception {
    OrderInfo orderInfo = new OrderInfo();
    orderInfo.setUsername(paymentInfoDto.getUserName());
    orderInfo.setOrderId(paymentInfoDto.getOrderId());
    orderInfoRepository.save(orderInfo);
  }

  //TODO: изменить с void
  void cancelPayment(Integer orderId) throws Exception {
    Optional<OrderStatusLog> orderStatusLog = orderStatusLogRepository.findById(orderId);

    if (!orderStatusLog.isPresent())
      throw new Exception("order not found: " + orderId);

    OrderStatusLog instanceLog = orderStatusLog.get();
    instanceLog.setNewStatus(OrderStatus.Canceled);
    orderStatusLogRepository.save(instanceLog);
  }
}

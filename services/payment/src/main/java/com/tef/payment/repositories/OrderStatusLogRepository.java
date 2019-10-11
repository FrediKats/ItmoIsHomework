package com.tef.payment.repositories;

import com.tef.payment.models.OrderStatusLog;
import org.springframework.data.repository.CrudRepository;

public interface OrderStatusLogRepository  extends CrudRepository<OrderStatusLog, Integer> {
}

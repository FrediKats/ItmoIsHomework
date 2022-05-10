package com.tef.payment.repositories;

import com.tef.payment.models.OrderInfo;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface OrderInfoRepository extends CrudRepository<OrderInfo, Integer> {
}

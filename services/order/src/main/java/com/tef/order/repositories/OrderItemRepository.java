package com.tef.order.repositories;

import com.tef.order.models.OrderItem;
import com.tef.order.types.OrderItemId;
import org.springframework.data.repository.CrudRepository;

public interface OrderItemRepository extends CrudRepository<OrderItem, OrderItemId>  {
}

package com.tef.order.controllers;

import com.tef.order.dtos.OrderDto;
import com.tef.order.services.OrderService;
import com.tef.order.types.OrderStatus;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class OrderController {
	private Logger logger = LoggerFactory.getLogger(OrderController.class);

	@Autowired
	private OrderService orderService;

	@GetMapping("api/orders")
	public List<OrderDto> getOrders() {
		logger.debug("getOrders");
		return orderService.getOrders();
	}

	@GetMapping("api/orders/{orderId}")
	public OrderDto getOrderById(@PathVariable Integer orderId) throws Exception {
		return orderService.getOrderById(orderId);
	}

	@PutMapping("api/orders/{orderId}/item/{itemId}")
	public void addItemToOrder(@PathVariable Integer orderId, @PathVariable Integer itemId) throws Exception {
		 orderService.addItemToOrder(orderId, itemId);
	}

	@PostMapping("api/orders/{orderId}/status/{status}")
	public void changeOrderStatus(@PathVariable Integer orderId, @PathVariable OrderStatus status) throws Exception {
		 orderService.changeOrderStatus(orderId, status);
	}

	@DeleteMapping("api/orders/{orderId}/remove/{itemId}")
	public void removeItemFromOrder(@PathVariable Integer orderId, @PathVariable Integer itemId) {
		 orderService.removeItemFromOrder(orderId, itemId);
	}
}

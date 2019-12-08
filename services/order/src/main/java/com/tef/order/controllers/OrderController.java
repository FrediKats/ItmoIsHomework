package com.tef.order.controllers;

import com.tef.order.dtos.OrderDto;
import com.tef.order.services.OrderService;
import com.tef.order.types.OrderStatus;
import org.springframework.cloud.stream.annotation.EnableBinding;
import org.springframework.cloud.stream.annotation.StreamListener;
import org.springframework.cloud.stream.messaging.Sink;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;


@RestController
public class OrderController {
	private final OrderService orderService;

	public OrderController(OrderService orderService) {
		this.orderService = orderService;
	}

	@GetMapping("api/orders")
	public List<OrderDto> getOrders() {
		return orderService.getOrders();
	}

	@GetMapping("api/orders/{orderId}")
	public OrderDto getOrderById(@PathVariable Integer orderId) throws Exception {
		return orderService.getOrderById(orderId);
	}

	@PutMapping("api/orders/{orderId}/item/{itemId}")
	public void addItemToOrder(@PathVariable Integer orderId, @PathVariable Integer itemId) throws Exception {
		//TODO: dirty hacks. validation if orderId passed
		if (orderId == -1)
			orderService.addItemToOrder(Optional.empty(), itemId);
		else
			orderService.addItemToOrder(Optional.of(orderId), itemId);
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

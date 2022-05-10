package com.tef.zuulproxy;

import com.netflix.discovery.EurekaClient;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

import java.util.HashMap;

@RestController
public class GatewayController {
    @Autowired
    private EurekaClient discoveryClient;

    private Logger logger = LoggerFactory.getLogger(GatewayController.class);

    @GetMapping("api/report/items/")
    public HashMap<Integer, ItemReport> getItemById() throws Exception {
        var items = getItems();
        var orders = getOrders();


        HashMap<Integer, ItemReport> reportMapper = new HashMap<>();

        for (int i = 0; i < items.length; i++) {
            ItemReport report = new ItemReport();
            report.setInWarehouse(items[i].getAmount());
            reportMapper.put(items[i].getId(), report);
        }

        for (int i = 0; i < orders.length; i++) {
            var orderItems = orders[i].getItems();
            for (int j = 0; j < orderItems.size(); j++) {
                ItemReport report = new ItemReport();
                if (reportMapper.containsKey(orderItems.get(j).getId())) {
                    report = reportMapper.get(orderItems.get(j).getId());
                }
                report.setInOrder(report.getInOrder() + orderItems.get(j).getAmount());
            }
        }
        reportMapper.forEach((a,b) -> b.setPercentInOrder((double)b.getInOrder() / (b.getInOrder() + b.getInWarehouse())));
        return reportMapper;
    }

    private ItemDto[] getItems() {
        var client = discoveryClient.getNextServerFromEureka("warehouse-client", false);

        String getItemUrl = client.getHomePageUrl() + "/api/warehouse/items/";
        ItemDto[] items =  new RestTemplate().getForObject(getItemUrl, ItemDto[].class);
        return items;
    }

    private OrderDto[] getOrders() {
        var client = discoveryClient.getNextServerFromEureka("order-client", false);

        String getItemUrl = client.getHomePageUrl() + "/api/orders/";
        OrderDto[] orders =  new RestTemplate().getForObject(getItemUrl, OrderDto[].class);
        return orders;
    }
}

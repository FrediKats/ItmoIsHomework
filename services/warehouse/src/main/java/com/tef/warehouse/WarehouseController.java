package com.tef.warehouse;

import com.tef.warehouse.dto.ItemCreationDto;
import com.tef.warehouse.dto.ItemDto;
import com.tef.warehouse.services.WarehouseService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@RestController
public class WarehouseController {
    @Autowired
    private WarehouseService warehouseService;

    @PutMapping("api/warehouse/items/{id}/addition/{amount}")
    public ItemDto addProduct(@PathVariable Integer id, @PathVariable Integer amount) throws Exception {
        return warehouseService.addProduct(id, amount);
    }

    @GetMapping("api/warehouse/items")
    public List<ItemDto> getItems() {
        logger.debug("Get items");
        return warehouseService.getItems();
    }

    @GetMapping("api/warehouse/items/{id}")
    public ItemDto getItemById(@PathVariable Integer id) throws Exception {
        return warehouseService.getItemById(id);
    }

    @PostMapping("api/warehouse/items")
    public ItemDto createProduct(ItemCreationDto item) {
        return warehouseService.createProduct(item);
    }

    @PutMapping("api/warehouse/items")
    public ItemDto removeProduct(Integer id, Integer amount) throws Exception {
        return warehouseService.removeProduct(id, amount);
    }
}

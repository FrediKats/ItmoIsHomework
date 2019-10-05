package com.tef.warehouse;

import com.tef.warehouse.dto.ItemCreationDto;
import com.tef.warehouse.dto.ItemDto;
import com.tef.warehouse.services.WarehouseService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class WarehouseController {
    private WarehouseService warehouseService;

    public WarehouseController() {
        warehouseService = new WarehouseService();
    }

    @PutMapping("api/warehouse/items/{id}/addition/{amount}")
    public ItemDto addProduct(@PathVariable Integer id, @PathVariable Integer amount) throws Exception {
        return warehouseService.addProduct(id, amount);
    }

    @GetMapping("api/warehouse/items")
    public List<ItemDto> getItems() {
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

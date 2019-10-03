package com.tef.warehouse;

import com.tef.warehouse.dto.ItemCreationDto;
import com.tef.warehouse.dto.ItemDto;
import org.springframework.web.bind.annotation.*;

@RestController
public class WarehouseController {

    @PutMapping("api/warehouse/items/{id}/addition/{amount}")
    public ItemDto AddProduct(@PathVariable Integer id, @PathVariable Integer amount)
    {
        //TODO: Implement
        throw new RuntimeException();
    }

    @GetMapping("api/warehouse/items")
    public ItemDto[] GetItem()
    {
        //TODO: Implement
        throw new RuntimeException();
    }

    @GetMapping("api/warehouse/items/{id}")
    public ItemDto GetItemById(@PathVariable Integer id)
    {
        //TODO: Implement
        throw new RuntimeException();
    }

    @PostMapping("api/warehouse/items")
    public ItemDto CreateProduct(ItemCreationDto item)
    {
        //TODO: Implement
        throw new RuntimeException();
    }

    @PutMapping("api/warehouse/items")
    public ItemDto RemoveProduct(Integer itemId, Integer amount)
    {
        //TODO: Implement
        throw new RuntimeException();
    }
}

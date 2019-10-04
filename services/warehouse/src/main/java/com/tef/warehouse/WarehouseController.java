package com.tef.warehouse;

import com.tef.warehouse.dto.ItemCreationDto;
import com.tef.warehouse.dto.ItemDto;
import com.tef.warehouse.models.Item;
import com.tef.warehouse.repositories.ItemRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@RestController
public class WarehouseController {
    @Autowired
    private ItemRepository itemRepository;

    @PutMapping("api/warehouse/items/{id}/addition/{amount}")
    public ItemDto AddProduct(@PathVariable Integer id, @PathVariable Integer amount)
    {
        Item item = itemRepository
                .findById(id)
                .get();

        item.setAmount(item.getAmount() + amount);
        return ItemDto.fromItem(itemRepository.save(item));
    }

    @GetMapping("api/warehouse/items")
    public List<ItemDto> GetItems()
    {
        return StreamSupport
                .stream(itemRepository
                        .findAll()
                        .spliterator(), false)
                .map(ItemDto::fromItem)
                .collect(Collectors.toList());
    }

    @GetMapping("api/warehouse/items/{id}")
    public ItemDto GetItemById(@PathVariable Integer id)
    {
        return ItemDto.fromItem(
                itemRepository
                        .findById(id)
                        .get());
    }

    @PostMapping("api/warehouse/items")
    public ItemDto CreateProduct(ItemCreationDto item)
    {
        return ItemDto.fromItem(
                itemRepository.save(
                        item.toItem()));
    }

    @PutMapping("api/warehouse/items")
    public ItemDto RemoveProduct(Integer id, Integer amount)
    {
        Item item = itemRepository
                .findById(id)
                .get();

        item.setAmount(item.getAmount() - amount);
        return ItemDto.fromItem(itemRepository.save(item));
    }
}

package com.tef.warehouse.services;

import com.tef.warehouse.dto.ItemCreationDto;
import com.tef.warehouse.dto.ItemDto;
import com.tef.warehouse.models.Item;
import com.tef.warehouse.repositories.ItemRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.PathVariable;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Service
public class WarehouseService {
    @Autowired
    private ItemRepository itemRepository;
    private Logger logger = LoggerFactory.getLogger(WarehouseService.class);
    public ItemDto addProduct(@PathVariable Integer id, @PathVariable Integer amount) throws Exception {
        Optional<Item> item = itemRepository.findById(id);
        if (item.isEmpty())
            throw new Exception("Item not found: " + id);

        Item instance = item.get();
        instance.setAmount(instance.getAmount() + amount);
        return ItemDto.fromItem(itemRepository.save(instance));
    }

    public List<ItemDto> getItems() {

        logger.debug("Get items");
        return StreamSupport
                .stream(itemRepository
                        .findAll()
                        .spliterator(), false)
                .map(ItemDto::fromItem)
                .collect(Collectors.toList());
    }

    public ItemDto getItemById(@PathVariable Integer id) throws Exception {
        Optional<Item> item = itemRepository.findById(id);
        if (item.isEmpty())
            throw new Exception("Item not found: " + id);

        return ItemDto.fromItem(item.get());
    }

    public ItemDto createProduct(ItemCreationDto item) {
        return ItemDto.fromItem(
                itemRepository.save(
                        item.toItem()));
    }

    public ItemDto removeProduct(Integer id, Integer amount) throws Exception {
        Optional<Item> item = itemRepository.findById(id);
        if (item.isEmpty())
            throw new Exception("Item not found: " + id);

        Item instance = item.get();
        instance.setAmount(instance.getAmount() - amount);
        return ItemDto.fromItem(itemRepository.save(instance));
    }
}

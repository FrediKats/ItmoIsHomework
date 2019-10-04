package com.tef.warehouse.repositories;

import com.tef.warehouse.models.Item;
import org.springframework.data.repository.CrudRepository;

public interface ItemRepository extends CrudRepository<Item, Integer> {

}
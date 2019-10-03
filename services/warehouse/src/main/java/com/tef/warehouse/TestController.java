package com.tef.warehouse;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class TestController {
    @GetMapping("api/warehouse/test")
    public String GetItem()
    {
        return "Ok";
    }
}

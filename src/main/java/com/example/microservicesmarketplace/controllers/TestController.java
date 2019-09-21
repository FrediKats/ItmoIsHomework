package com.example.microservicesmarketplace.controllers;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
class TestController {

    @GetMapping("/test")
    String test() {
        return "repository.findAll()";
    }
}

package com.tef.euservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.eureka.server.EnableEurekaServer;

@SpringBootApplication
@EnableEurekaServer
public class EuserviceApplication {

	public static void main(String[] args) {
		SpringApplication.run(EuserviceApplication.class, args);
	}

}

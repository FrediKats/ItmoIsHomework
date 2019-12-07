package com.tef.order;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.context.support.ConversionServiceFactoryBean;
import org.springframework.core.convert.ConversionService;
import springfox.documentation.swagger2.annotations.EnableSwagger2;

@SpringBootApplication
@EnableSwagger2
public class OrderApplication {

    public static void main(String[] args) {
        SpringApplication.run(OrderApplication.class, args);
    }

    @Bean(name = "conversionService")
    @Qualifier("webFluxConversionService")
    public ConversionService getConversionService() {
        ConversionServiceFactoryBean bean = new ConversionServiceFactoryBean();
        bean.afterPropertiesSet();
        return bean.getObject();
    }
}

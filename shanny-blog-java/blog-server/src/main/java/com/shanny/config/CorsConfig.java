package com.shanny.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

@Configuration
public class CorsConfig {
    @Bean
    public WebMvcConfigurer corsConfigurer() {
        return new WebMvcConfigurer() {
            @Override
            public void addCorsMappings(CorsRegistry registry) {
                registry.addMapping("/**")  // 匹配所有路径
                        .allowedOrigins(
                                "https://www.shanny.work",
                                "https://shanny.work",
                                "http://localhost:5174",
                                "http://localhost:5173"
                        )  // 允许的前端域
                        .allowedMethods("*")  // 允许所有的请求方法
                        .allowedHeaders("*")  // 允许的请求头
                        .allowCredentials(true);  // 允许携带 Cookie
            }
        };
    }
}

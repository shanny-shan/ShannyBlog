package com.shanny.config;

import com.shanny.interceptor.JwtTokenUserInterceptor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.*;

/**
 * 配置类，注册web层相关组件
 */
@Configuration
@Slf4j
public class WebMvcConfiguration implements WebMvcConfigurer {
    private final JwtTokenUserInterceptor jwtTokenUserInterceptor;
    @Autowired
    public WebMvcConfiguration(JwtTokenUserInterceptor jwtTokenUserInterceptor) {
        this.jwtTokenUserInterceptor = jwtTokenUserInterceptor;
    }

    @Override
    public void addInterceptors(InterceptorRegistry registry) {
        log.info("开始注册自定义拦截器...");
        registry.addInterceptor(jwtTokenUserInterceptor)
                .addPathPatterns("/**")
                .excludePathPatterns("/account/login")
                .excludePathPatterns("/account/register")
                .excludePathPatterns("/about/show")
                .excludePathPatterns("/article/all")
                .excludePathPatterns("/article/type")
                .excludePathPatterns("/article/id")
                .excludePathPatterns("/category/all")
                .excludePathPatterns("/tag/all")
                .excludePathPatterns("/project/info")
                .excludePathPatterns("/tool/all");
    }
}

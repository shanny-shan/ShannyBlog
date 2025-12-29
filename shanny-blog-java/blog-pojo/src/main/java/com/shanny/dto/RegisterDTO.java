package com.shanny.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Data;

@Data
@Schema(description = "注册时提交的数据")
public class RegisterDTO {
    private String userId;
    private String mobile;
    private String password;
}

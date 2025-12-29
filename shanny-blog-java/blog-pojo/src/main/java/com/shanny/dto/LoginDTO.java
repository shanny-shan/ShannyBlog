package com.shanny.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Data;

@Data
@Schema(description = "登陆时提交的数据")
public class LoginDTO {
    private String userId;
    private String password;
}

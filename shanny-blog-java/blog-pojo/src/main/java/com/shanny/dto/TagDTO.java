package com.shanny.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Data;

@Data
@Schema(description = "添加作者信息提交的数据")
public class TagDTO {
    private String name;
    private String nameEn;
}

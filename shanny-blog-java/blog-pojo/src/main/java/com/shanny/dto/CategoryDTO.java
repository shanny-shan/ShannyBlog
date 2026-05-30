package com.shanny.dto;

import com.shanny.enums.CategoryEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Data;

@Data
@Schema(description = "添加菜单时提交的数据")
public class CategoryDTO {
    private Long id;
    private String name;
    private String nameEn;
    private CategoryEnum.CategoryType type;
    private int sort;
}

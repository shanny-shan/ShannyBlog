package com.shanny.dto;

import com.shanny.converter.LongListConverter;
import com.shanny.enums.CategoryEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.Data;

import java.util.List;

@Data
@Schema(description = "添加文章提交的数据")
public class ArticleDTO {
    private String title;
    private String content;
    private String memo;
    private String image;
    private String href;
    private List<Long> tags;
    private CategoryEnum.CategoryType type;
    private Long categoryId;
    private List<Long> timelines;
    private Integer views = 0;
    private Boolean published = true;
}

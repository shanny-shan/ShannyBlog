package com.shanny.vo;

import com.shanny.converter.LongListConverter;
import com.shanny.entity.Tag;
import com.shanny.enums.CategoryEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDateTime;
import java.util.List;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "文章信息")
public class ArticleVO {
    private Long id;
    private String title;
    private String memo;
    private String content;
    private String image;
    private String href;
    private List<Long> tags;
    private List<Tag> tagList;
    private CategoryEnum.CategoryType type;
    private Long categoryId;
    private List<Long> timelines;
    private Integer views;
    private Boolean published;
    private LocalDateTime createTime;
    private LocalDateTime updateTime;
}

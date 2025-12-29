package com.shanny.vo;

import com.shanny.converter.LongListConverter;
import com.shanny.entity.Tag;
import com.shanny.enums.CategoryEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.Column;
import jakarta.persistence.Convert;
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
@Schema(description = "工具信息")
public class ToolVO {
    private Long id;
    private String title;
    private String content;
    private String image;
    private String href;
    private List<Long> tags;
    private List<Tag> tagList;
    private Boolean published;
    private LocalDateTime createTime;
    private LocalDateTime updateTime;
}

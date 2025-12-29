package com.shanny.dto;

import com.shanny.converter.LongListConverter;
import com.shanny.enums.CategoryEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.Column;
import jakarta.persistence.Convert;
import jakarta.validation.constraints.NotNull;
import lombok.Data;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDateTime;
import java.util.List;

@Data
@Schema(description = "添加工具提交的数据")
public class ToolDTO {
    private String title;
    private String content;
    private String image;
    private String href;
    private List<Long> tags;
}

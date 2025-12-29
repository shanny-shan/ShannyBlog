package com.shanny.vo;

import com.shanny.enums.AboutEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDateTime;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "作者信息")
public class AboutVO {
    private Long id;
    private String name;
    private String introduce;
    private String avatar;
    private String tag;
    private String github;
    private String steam;
    private String web;
    private String biliBili;
    private Boolean isActive;
    private String other;
    private LocalDateTime createTime;
    private LocalDateTime updateTime;
}

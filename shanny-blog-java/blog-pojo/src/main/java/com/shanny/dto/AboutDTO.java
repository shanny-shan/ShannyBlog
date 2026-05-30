package com.shanny.dto;

import com.shanny.enums.AboutEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@Schema(description = "添加作者信息提交的数据")
public class AboutDTO {
    private Long id;
    private String avatar;
    private String name;
    private String introduce;
    private String tag;
    private String github;
    private String steam;
    private String web;
    private String biliBili;
    private Boolean isActive;
    private String other;
}

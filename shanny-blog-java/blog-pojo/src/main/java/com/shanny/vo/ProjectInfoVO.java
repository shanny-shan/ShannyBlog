package com.shanny.vo;

import com.shanny.entity.Tag;
import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;
import java.util.List;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "工具信息")
public class ProjectInfoVO {
    private String name;
    private String description;
    private String owner;
    private String version;
    private String buildTime;
}

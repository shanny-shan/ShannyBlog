package com.shanny.vo;

import com.shanny.entity.UserDetails;
import com.shanny.enums.CategoryEnum;
import com.shanny.enums.UserEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.Column;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "菜单信息")
public class CategoryVO {
    private Long id;
    private String name;
    private String nameEn;
    private CategoryEnum.CategoryType type;
    private Integer sort;
}

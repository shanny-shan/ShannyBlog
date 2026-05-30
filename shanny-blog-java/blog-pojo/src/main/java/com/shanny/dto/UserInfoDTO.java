package com.shanny.dto;

import com.shanny.entity.UserDetails;
import com.shanny.enums.UserEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;
import java.time.LocalDateTime;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "用户基础信息")
public class UserInfoDTO {
    private String uuid;
    private String userId;
    private String mobile;
    private String password;
    private UserEnum.UserStatus status;
    private UserEnum.UserType type;
    private UserDetails userDetails;
}

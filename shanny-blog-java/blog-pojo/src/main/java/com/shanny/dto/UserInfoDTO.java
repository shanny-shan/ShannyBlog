package com.shanny.dto;

import com.shanny.enums.UserEnum;
import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "用户基础信息")
public class UserDetailDTO {
    private String uuid;
    private String nickname;
    private String username;
    private LocalDate birthday;
    private UserEnum.UserSex sex;
    private String avatar;
}

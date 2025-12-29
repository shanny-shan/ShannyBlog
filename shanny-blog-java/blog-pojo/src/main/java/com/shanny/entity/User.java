package com.shanny.entity;

import com.shanny.enums.UserEnum;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.Data;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDateTime;

@Data
@Entity
@Table(name = "users")
public class User {
    @Id
    @Column(length = 50)
    private String uuid;

    @Column(length = 30, nullable = false)
    @NotNull
    private String userId;

    @Column(length = 11, nullable = false)
    @NotNull
    private String mobile;

    @Column(length = 100, nullable = false)
    @NotNull
    private String password;

    @Column(nullable = false)
    @NotNull
    @Enumerated(EnumType.STRING)
    private UserEnum.UserStatus status;

    @Column(nullable = false)
    @NotNull
    @Enumerated(EnumType.STRING)
    private UserEnum.UserType type;

    @Column(nullable = false)
    @NotNull
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private LocalDateTime createTime;

    @Column(nullable = false)
    @NotNull
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private LocalDateTime updateTime;

    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private LocalDateTime lastLoginTime;
}



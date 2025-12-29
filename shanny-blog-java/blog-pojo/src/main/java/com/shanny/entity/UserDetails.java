package com.shanny.entity;

import com.shanny.enums.UserEnum.*;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.Data;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDate;
import java.time.LocalDateTime;

@Data
@Entity
@Table(name = "user_details")
public class UserDetails {
    @Id
    @Column(length = 50, nullable = false)
    @NotNull
    private String uuid;

    @Column(length = 30)
    private String nickname;

    @Column(length = 30)
    private String username;

    @DateTimeFormat(pattern = "yyyy-MM-dd")
    private LocalDate birthday;

    @Column(nullable = false)
    @NotNull
    @Enumerated(EnumType.STRING)
    private UserSex sex;

    @Column(length = 500)
    private String avatar;

    @Column(nullable = false)
    @NotNull
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private LocalDateTime createTime;

    @Column(nullable = false)
    @NotNull
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private LocalDateTime updateTime;
}

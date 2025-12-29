package com.shanny.entity;

import com.shanny.enums.AboutEnum;
import com.shanny.enums.UserEnum;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.Data;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDateTime;

@Data
@Entity
@Table(name = "abouts")
public class About {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @NotNull
    private Boolean isActive;

    @Column(length = 30, nullable = false)
    @NotNull
    private String name;

    @Column(length = 300, nullable = false)
    @NotNull
    private String introduce;

    @Column(length = 500, nullable = false)
    @NotNull
    private String avatar;

    @Column(length = 200, nullable = false)
    @NotNull
    private String tag;

    @Column(length = 200)
    private String github;

    @Column(length = 200)
    private String steam;

    @Column(length = 200)
    private String web;

    @Column(length = 200)
    private String biliBili;

    @Column(length = 200)
    private String other;

    @Column(nullable = false)
    @NotNull
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private LocalDateTime createTime;

    @Column(nullable = false)
    @NotNull
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private LocalDateTime updateTime;
}



package com.shanny.entity;

import com.shanny.converter.LongListConverter;
import com.shanny.enums.ArticleEnum;
import com.shanny.enums.CategoryEnum;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.Data;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDateTime;
import java.util.List;

@Data
@Entity
@Table(name = "articles")
public class Article {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @NotNull
    @Column(length = 50, nullable = false)
    private String title;

    @NotNull
    @Lob
    @Column(columnDefinition = "TEXT", nullable = false)
    private String content;

    @Column(columnDefinition = "TEXT", nullable = false)
    private String memo;

    @Column(length = 300)
    private String image;

    @Column(length = 300)
    private String href;

    @Convert(converter = LongListConverter.class)
    private List<Long> tags;

    @NotNull
    @Column(nullable = false)
    @Enumerated(EnumType.STRING)
    private CategoryEnum.CategoryType type; // 内容类型

    @Column()
    private Long categoryId; // 内容分类

    @NotNull
    @Column(nullable = false)
    @Convert(converter = LongListConverter.class)
    private List<Long> timelines;

    @NotNull
    @Column(nullable = false)
    private Integer views = 0;

    @NotNull
    @Column(nullable = false)
    private Boolean published = true;

    @NotNull
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    @Column(nullable = false)
    private LocalDateTime createTime;

    @NotNull
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    @Column(nullable = false)
    private LocalDateTime updateTime;
}



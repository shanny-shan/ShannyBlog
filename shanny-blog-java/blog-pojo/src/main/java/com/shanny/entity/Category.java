package com.shanny.entity;

import com.shanny.enums.ArticleEnum;
import com.shanny.enums.CategoryEnum;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import lombok.Data;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDateTime;

@Data
@Entity
@Table(name = "categories")
public class Category {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @NotNull
    @Column(length = 10, nullable = false)
    private String name;

    @NotNull
    @Column(length = 10, nullable = false)
    private String nameEn;

    @NotNull
    @Column(nullable = false)
    @Enumerated(EnumType.STRING)
    private CategoryEnum.CategoryType type;

    @NotNull
    @Column(nullable = false)
    private int sort = 0;
}



package com.shanny.entity;

import com.shanny.converter.LongListConverter;
import com.shanny.enums.MediaEnum;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.Data;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDateTime;
import java.util.List;

@Data
@Entity
@Table(name = "medias")
public class Media {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @NotNull
    @Column(length = 50, nullable = false)
    private String title;

    @Column(length = 300)
    private String content;

    @Column(length = 300)
    private String image;

    @NotNull
    @Column(length = 300, nullable = false)
    private String href;

    @Convert(converter = LongListConverter.class)
    private List<Long> tags;

    @NotNull
    @Column(nullable = false)
    @Enumerated(EnumType.STRING)
    private MediaEnum.MediaType type;

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



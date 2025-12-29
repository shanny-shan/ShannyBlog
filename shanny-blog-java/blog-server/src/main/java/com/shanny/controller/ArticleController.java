package com.shanny.controller;

import com.shanny.dto.ArticleDTO;
import com.shanny.enums.CategoryEnum;
import com.shanny.result.Result;
import com.shanny.service.ArticleService;
import com.shanny.vo.ArticleVO;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController()
@RequestMapping("/article")
@Tag(name = "文章相关接口")
@Slf4j
public class ArticleController {
    private final ArticleService articleService;

    @Autowired
    public ArticleController(ArticleService articleService) {
        this.articleService = articleService;
    }

    @GetMapping("/all")
    @Operation(summary = "文章获取")
    public Result<List<ArticleVO>> GetCategories() {
        try {
            return articleService.getArticles();
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @GetMapping("/type")
    @Operation(summary = "文章获取")
    public Result<List<ArticleVO>> GetCategories(CategoryEnum.CategoryType type) {
        try {
            return articleService.getArticlesByType(type);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/add")
    @Operation(summary = "文章添加")
    public Result<ArticleVO> AddAArticle(@RequestBody ArticleDTO articleDTO) {
        try {
            return articleService.addArticle(articleDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }
}

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
import org.springframework.web.multipart.MultipartFile;

import java.util.List;
import java.util.Map;

import static com.shanny.constant.ResultConstant.DELETE_FAIL;
import static com.shanny.constant.ResultConstant.DELETE_SUCCESS;

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

    @GetMapping("/recent")
    @Operation(summary = "文章获取")
    public Result<List<ArticleVO>> GetArticlesByRecent() {
        try {
            return articleService.getArticlesByRecent();
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @GetMapping("/views")
    @Operation(summary = "文章获取")
    public Result<List<ArticleVO>> GetArticlesByView() {
        try {
            return articleService.getArticlesByView();
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @GetMapping("/type")
    @Operation(summary = "文章获取")
    public Result<List<ArticleVO>> GetArticleByType(CategoryEnum.CategoryType type) {
        try {
            return articleService.getArticlesByType(type);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @GetMapping("/id")
    @Operation(summary = "文章获取")
    public Result<ArticleVO> GetArticleById(Long id) {
        try {
            return articleService.getArticleById(id);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/add")
    @Operation(summary = "文章添加")
    public Result<ArticleVO> AddArticle(@RequestBody ArticleDTO articleDTO) {
        try {
            return articleService.addArticle(articleDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/update")
    @Operation(summary = "文章修改")
    public Result<ArticleVO> UpdateArticle(@RequestBody ArticleDTO articleDTO){
        try {
            return articleService.updateArticle(articleDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/delete")
    @Operation(summary = "文章删除")
    public Result<String> deleteArticle(Long id) {
        try {
            return articleService.deleteArticle(id);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }
}

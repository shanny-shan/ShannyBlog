package com.shanny.controller;

import com.shanny.dto.CategoryDTO;
import com.shanny.result.Result;
import com.shanny.service.CategoryService;
import com.shanny.vo.CategoryVO;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController()
@RequestMapping("/category")
@Tag(name = "菜单相关接口")
@Slf4j
public class CategoryController {
    private final CategoryService categoryService;

    @Autowired
    public CategoryController(CategoryService categoryService) {
        this.categoryService = categoryService;
    }

    @GetMapping("/all")
    @Operation(summary = "菜单获取")
    public Result<List<CategoryVO>> GetCategories() {
        try {
            return categoryService.getCategories();
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/add")
    @Operation(summary = "菜单添加")
    public Result<CategoryVO> AddCategory(@RequestBody CategoryDTO categoryDTO) {
        try {
            return categoryService.addCategory(categoryDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

}

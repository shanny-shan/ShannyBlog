package com.shanny.controller;

import com.shanny.dto.CategoryDTO;
import com.shanny.dto.TagDTO;
import com.shanny.enums.CategoryEnum;
import com.shanny.result.Result;
import com.shanny.service.TagService;
import com.shanny.vo.ArticleVO;
import com.shanny.vo.CategoryVO;
import com.shanny.vo.TagVO;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController()
@RequestMapping("/tag")
@Tag(name = "菜单相关接口")
@Slf4j
public class TagController {
    private final TagService tagService;

    @Autowired
    public TagController(TagService tagService) {
        this.tagService = tagService;
    }

    @GetMapping("/all")
    @Operation(summary = "标签获取")
    public Result<List<TagVO>> GetTags() {
        try {
            return tagService.getTags();
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @GetMapping("/id")
    @Operation(summary = "通过id获取标签")
    public Result<TagVO> GetTagsById(Long id) {
        try {
            return tagService.getTagsById(id);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/add")
    @Operation(summary = "标签添加")
    public Result<TagVO> AddAboutMe(@RequestBody TagDTO tagDTO) {
        try {
            return tagService.addTag(tagDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/update")
    @Operation(summary = "作者信息修改")
    public Result<TagVO> UpdateTag(@RequestBody TagDTO tagDTO) {
        try {
            return tagService.updateTag(tagDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/delete")
    @Operation(summary = "作者信息删除")
    public Result<String> deleteTag(Long id) {
        try {
            return tagService.deleteTagById(id);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }
}

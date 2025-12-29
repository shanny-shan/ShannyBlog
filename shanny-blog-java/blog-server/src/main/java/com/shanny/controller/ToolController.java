package com.shanny.controller;

import com.shanny.dto.ArticleDTO;
import com.shanny.dto.ToolDTO;
import com.shanny.enums.CategoryEnum;
import com.shanny.result.Result;
import com.shanny.service.ArticleService;
import com.shanny.service.ToolService;
import com.shanny.vo.ArticleVO;
import com.shanny.vo.ToolVO;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController()
@RequestMapping("/tool")
@Tag(name = "工具相关接口")
@Slf4j
public class ToolController {
    private final ToolService toolService;

    @Autowired
    public ToolController(ToolService toolService) {
        this.toolService = toolService;
    }

    @GetMapping("/all")
    @Operation(summary = "工具获取")
    public Result<List<ToolVO>> GetTools() {
        try {
            return toolService.getTools();
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/add")
    @Operation(summary = "工具添加")
    public Result<ToolVO> AddTool(@RequestBody ToolDTO toolDTO) {
        try {
            return toolService.addTool(toolDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }
}

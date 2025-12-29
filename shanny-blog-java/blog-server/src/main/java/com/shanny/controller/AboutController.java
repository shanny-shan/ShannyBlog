package com.shanny.controller;

import com.shanny.dto.AboutDTO;
import com.shanny.result.Result;
import com.shanny.service.AboutService;
import com.shanny.vo.AboutVO;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController()
@RequestMapping("/about")
@Tag(name = "菜单相关接口")
@Slf4j
public class AboutController {
    private final AboutService aboutService;

    @Autowired
    public AboutController(AboutService aboutService) {
        this.aboutService = aboutService;
    }

    @GetMapping("/all")
    @Operation(summary = "作者信息获取")
    public Result<List<AboutVO>> GetAboutMe() {
        try {
            return aboutService.getAboutMe();
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @GetMapping("/show")
    @Operation(summary = "作者信息获取")
    public Result<AboutVO> GetAboutMeByShow() {
        try {
            return aboutService.getAboutMeByShow();
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/add")
    @Operation(summary = "作者信息添加")
    public Result<AboutVO> AddAboutMe(@RequestBody AboutDTO aboutDTO) {
        try {
            return aboutService.addAbout(aboutDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }
}

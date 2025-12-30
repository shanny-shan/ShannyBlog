package com.shanny.controller;

import com.shanny.dto.ToolDTO;
import com.shanny.properties.AppProperties;
import com.shanny.result.Result;
import com.shanny.service.ToolService;
import com.shanny.vo.ProjectInfoVO;
import com.shanny.vo.TagVO;
import com.shanny.vo.ToolVO;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

import static com.shanny.constant.ResultConstant.INSERT_SUCCESS;

@RestController()
@RequestMapping("/project")
@Tag(name = "项目信息")
@Slf4j
public class ProjectInfoController {

    @Autowired
    private AppProperties appProperties;

    @GetMapping("/info")
    public Result<ProjectInfoVO> info() {
        ProjectInfoVO vo = new ProjectInfoVO();
        vo.setName(appProperties.getName());
        vo.setDescription(appProperties.getDescription());
        vo.setOwner(appProperties.getOwner());
        vo.setVersion(appProperties.getVersion());
        vo.setBuildTime(appProperties.getBuildTime());
        return Result.success(INSERT_SUCCESS, vo);
    }
}

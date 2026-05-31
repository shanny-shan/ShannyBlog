package com.shanny.service.impl;

import com.shanny.dto.ArticleDTO;
import com.shanny.dto.ToolDTO;
import com.shanny.entity.Article;
import com.shanny.entity.Tag;
import com.shanny.entity.Tool;
import com.shanny.enums.CategoryEnum;
import com.shanny.mapper.ArticleMapper;
import com.shanny.mapper.TagMapper;
import com.shanny.mapper.ToolMapper;
import com.shanny.result.Result;
import com.shanny.service.ArticleService;
import com.shanny.service.ToolService;
import com.shanny.vo.ArticleVO;
import com.shanny.vo.ToolVO;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

import static com.shanny.constant.ResultConstant.*;
import static com.shanny.constant.ResultConstant.DELETE_FAIL;
import static com.shanny.constant.ResultConstant.DELETE_SUCCESS;
import static com.shanny.constant.ResultConstant.UPDATE_SUCCESS;

@Service
public class ToolServiceImpl implements ToolService {
    private final ToolMapper toolMapper;
    private final TagMapper tagMapper;

    @Autowired
    public ToolServiceImpl(ToolMapper toolMapper, TagMapper tagMapper) {
        this.toolMapper = toolMapper;
        this.tagMapper = tagMapper;
    }

    @Override
    public Result<List<ToolVO>> getTools() {
        List<Tool> tools = toolMapper.getAll();
        List<ToolVO> toolVOList = tools.stream().map(tool -> {
            ToolVO vo = new ToolVO();
            BeanUtils.copyProperties(tool, vo);
            vo.setTagList(new ArrayList<>());
            if (tool.getTags() != null) {
                for (Long tagId : tool.getTags()) {
                    Tag tag = tagMapper.getById(tagId);
                    if (tag != null) {
                        vo.getTagList().add(tag);
                    }
                }
            }
            return vo;
        }).toList();
        return Result.success(SELECT_SUCCESS, toolVOList);
    }

    @Override
    public Result<ToolVO> addTool(ToolDTO toolDTO) {
        Tool tool = new Tool();
        BeanUtils.copyProperties(toolDTO, tool);

        String src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
        int randomNum = (int) (Math.random() * 6) + 1;
        tool.setImage(src + randomNum + ".jpg");

        toolMapper.insert_tool(tool);

        ToolVO toolVO = new ToolVO();
        BeanUtils.copyProperties(tool, toolVO);

        return Result.success(INSERT_SUCCESS, toolVO);
    }

    @Override
    public Result<ToolVO> updateTool(ToolDTO toolDTO) {
        if(toolDTO.getId() == null){
            return Result.error(UPDATE_FAIL);
        }

        Tool tool = new Tool();
        BeanUtils.copyProperties(toolDTO, tool);
        toolMapper.update_tool(tool);

        ToolVO toolVO = new ToolVO();
        BeanUtils.copyProperties(tool, toolVO);

        return Result.success(UPDATE_SUCCESS, toolVO);
    }

    @Override
    public Result<String> deleteTool(Long id) {
        if(id == null){
            return Result.error(DELETE_FAIL);
        }
        toolMapper.deleteById(id);
        return Result.success(DELETE_SUCCESS);
    }
}

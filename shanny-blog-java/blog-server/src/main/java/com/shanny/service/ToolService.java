package com.shanny.service;

import com.shanny.dto.ArticleDTO;
import com.shanny.dto.ToolDTO;
import com.shanny.enums.CategoryEnum;
import com.shanny.result.Result;
import com.shanny.vo.ArticleVO;
import com.shanny.vo.ToolVO;

import java.util.List;

public interface ToolService {

    Result<List<ToolVO>> getTools();

    Result<ToolVO> addTool(ToolDTO toolDTO);
}

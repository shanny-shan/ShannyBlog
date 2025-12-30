package com.shanny.service;

import com.shanny.dto.AboutDTO;
import com.shanny.dto.ArticleDTO;
import com.shanny.enums.CategoryEnum;
import com.shanny.result.Result;
import com.shanny.vo.AboutVO;
import com.shanny.vo.ArticleVO;

import java.util.List;

public interface ArticleService {

    Result<ArticleVO> addArticle(ArticleDTO articleDTO);

    Result<List<ArticleVO>> getArticles();

    Result<List<ArticleVO>> getArticlesByType(CategoryEnum.CategoryType type);

    Result<ArticleVO> getArticleById(Long id);
}

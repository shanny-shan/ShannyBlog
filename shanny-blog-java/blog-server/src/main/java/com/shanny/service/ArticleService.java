package com.shanny.service;

import com.shanny.dto.AboutDTO;
import com.shanny.dto.ArticleDTO;
import com.shanny.enums.CategoryEnum;
import com.shanny.result.Result;
import com.shanny.vo.AboutVO;
import com.shanny.vo.ArticleVO;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;

public interface ArticleService {

    Result<ArticleVO> addArticle(ArticleDTO articleDTO);

    Result<List<ArticleVO>> getArticlesByRecent();

    Result<List<ArticleVO>> getArticlesByType(CategoryEnum.CategoryType type);

    Result<ArticleVO> getArticleById(Long id);

    Result<ArticleVO> updateArticle(ArticleDTO articleDTO);

    Result<String> deleteArticle(Long id);

    Result<List<ArticleVO>> getArticlesByView();

    Result<List<ArticleVO>> getArticleByTag(Long tagId);
}

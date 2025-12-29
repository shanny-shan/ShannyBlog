package com.shanny.service.impl;

import com.shanny.dto.AboutDTO;
import com.shanny.dto.ArticleDTO;
import com.shanny.entity.About;
import com.shanny.entity.Article;
import com.shanny.entity.Category;
import com.shanny.entity.Tag;
import com.shanny.enums.CategoryEnum;
import com.shanny.mapper.AboutMapper;
import com.shanny.mapper.ArticleMapper;
import com.shanny.mapper.TagMapper;
import com.shanny.result.Result;
import com.shanny.service.AboutService;
import com.shanny.service.ArticleService;
import com.shanny.vo.AboutVO;
import com.shanny.vo.ArticleVO;
import com.shanny.vo.CategoryVO;
import com.shanny.vo.TagVO;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

import static com.shanny.constant.ResultConstant.INSERT_SUCCESS;
import static com.shanny.constant.ResultConstant.SELECT_SUCCESS;

@Service
public class ArticleServiceImpl implements ArticleService {
    private final ArticleMapper articleMapper;
    private final TagMapper tagMapper;

    @Autowired
    public ArticleServiceImpl(ArticleMapper articleMapper, TagMapper tagMapper) {
        this.articleMapper = articleMapper;
        this.tagMapper = tagMapper;
    }

    @Override
    public Result<ArticleVO> addArticle(ArticleDTO articleDTO) {
        Article article = new Article();
        BeanUtils.copyProperties(articleDTO, article);

        articleMapper.insert_article(article);

        ArticleVO articleVO = new ArticleVO();
        BeanUtils.copyProperties(article, articleVO);

        return Result.success(INSERT_SUCCESS, articleVO);
    }

    @Override
    public Result<List<ArticleVO>> getArticles(){
        List<Article> articles = articleMapper.getAll();
        List<ArticleVO> articleVOList = articles.stream().map(article -> {
            ArticleVO vo = new ArticleVO();
            BeanUtils.copyProperties(article, vo);
            return vo;
        }).toList();
        return Result.success(SELECT_SUCCESS, articleVOList);
    }

    @Override
    public Result<List<ArticleVO>> getArticlesByType(CategoryEnum.CategoryType type){
        List<Article> articles = articleMapper.getByType(type);
        List<ArticleVO> articleVOList = articles.stream().map(article -> {
            ArticleVO vo = new ArticleVO();
            BeanUtils.copyProperties(article, vo);
            vo.setTagList(new ArrayList<>());
            if (article.getTags() != null) {
                for (Long tagId : article.getTags()) {
                    Tag tag = tagMapper.getById(tagId);
                    if (tag != null) {
                        vo.getTagList().add(tag);
                    }
                }
            }
            return vo;
        }).toList();
        return Result.success(SELECT_SUCCESS, articleVOList);
    }
}

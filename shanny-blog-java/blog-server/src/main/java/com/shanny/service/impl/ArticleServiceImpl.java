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
import com.shanny.mapper.CategoryMapper;
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
import org.springframework.web.multipart.MultipartFile;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

import static com.shanny.constant.ResultConstant.*;

@Service
public class ArticleServiceImpl implements ArticleService {
    private final ArticleMapper articleMapper;
    private final TagMapper tagMapper;
    private final CategoryMapper categoryMapper;

    @Autowired
    public ArticleServiceImpl(ArticleMapper articleMapper, TagMapper tagMapper, CategoryMapper categoryMapper) {
        this.articleMapper = articleMapper;
        this.tagMapper = tagMapper;
        this.categoryMapper = categoryMapper;
    }

    @Override
    public Result<List<ArticleVO>> getArticlesByRecent(){
        List<Article> articles = articleMapper.getByRecent();

        if (articles == null || articles.isEmpty()) {
            return Result.success(SELECT_SUCCESS, new ArrayList<>());
        }

        List<ArticleVO> articleVOList = articles.stream()
                .map(article -> {
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
                    Category category = categoryMapper.getById(article.getCategoryId());
                    CategoryVO categoryVO = new CategoryVO();
                    BeanUtils.copyProperties(category, categoryVO);
                    vo.setCategory(categoryVO);
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
            Category category = categoryMapper.getById(article.getCategoryId());
            CategoryVO categoryVO = new CategoryVO();
            BeanUtils.copyProperties(category, categoryVO);
            vo.setCategory(categoryVO);
            return vo;
        }).toList();
        return Result.success(SELECT_SUCCESS, articleVOList);
    }

    @Override
    public Result<ArticleVO> getArticleById(Long id) {
        Article article = articleMapper.getById(id);

        article.setViews(article.getViews() + 1);
        articleMapper.update_article_views(article);

        ArticleVO vo = new ArticleVO();
        BeanUtils.copyProperties(article, vo);
        Category category = categoryMapper.getById(article.getCategoryId());
        CategoryVO categoryVO = new CategoryVO();
        BeanUtils.copyProperties(category, categoryVO);
        vo.setCategory(categoryVO);
        return Result.success(SELECT_SUCCESS, vo);
    }

    @Override
    public Result<ArticleVO> addArticle(ArticleDTO articleDTO) {
        Article article = new Article();
        BeanUtils.copyProperties(articleDTO, article);

        String src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
        int randomNum = (int) (Math.random() * 6) + 1;
        article.setImage(src + randomNum + ".jpg");
        article.setHref(src+ "5.jpg");

        articleMapper.insert_article(article);

        ArticleVO articleVO = new ArticleVO();
        BeanUtils.copyProperties(article, articleVO);

        return Result.success(INSERT_SUCCESS, articleVO);
    }

    @Override
    public Result<ArticleVO> updateArticle(ArticleDTO articleDTO) {
        if(articleDTO.getId() == null){
            return Result.error(UPDATE_FAIL);
        }

        Article article = new Article();
        BeanUtils.copyProperties(articleDTO, article);

        articleMapper.update_article(article);

        ArticleVO articleVO = new ArticleVO();
        BeanUtils.copyProperties(article, articleVO);

        return Result.success(UPDATE_SUCCESS, articleVO);
    }

    @Override
    public Result<String> deleteArticle(Long id) {
        if(id == null){
            return Result.error(DELETE_FAIL);
        }
        articleMapper.deleteById(id);
        return Result.success(DELETE_SUCCESS);
    }

    @Override
    public Result<List<ArticleVO>> getArticlesByView() {
        List<Article> articles = articleMapper.getByView();

        if (articles == null || articles.isEmpty()) {
            return Result.success(SELECT_SUCCESS, new ArrayList<>());
        }

        List<ArticleVO> articleVOList = articles.stream()
                .map(article -> {
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
                    Category category = categoryMapper.getById(article.getCategoryId());
                    CategoryVO categoryVO = new CategoryVO();
                    BeanUtils.copyProperties(category, categoryVO);
                    vo.setCategory(categoryVO);
                    return vo;
                }).toList();

        return Result.success(SELECT_SUCCESS, articleVOList);
    }
}

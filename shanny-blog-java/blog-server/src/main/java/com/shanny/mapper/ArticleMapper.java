package com.shanny.mapper;

import com.shanny.annotation.AutoFill;
import com.shanny.dto.ArticleDTO;
import com.shanny.entity.About;
import com.shanny.entity.Article;
import com.shanny.entity.Category;
import com.shanny.enums.AutoFillEnum;
import com.shanny.enums.CategoryEnum;
import org.apache.ibatis.annotations.Select;

import java.util.List;

public interface ArticleMapper {

    @AutoFill(AutoFillEnum.OperationType.INSERT)
    void insert_article(Article article);

    @Select("select * from shanny_blog.articles")
    List<Article> getAll();

    @Select("select * from shanny_blog.articles where type = #{type}")
    List<Article> getByType(CategoryEnum.CategoryType type);
}

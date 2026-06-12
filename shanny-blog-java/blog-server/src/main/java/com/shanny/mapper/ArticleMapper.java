package com.shanny.mapper;

import com.shanny.annotation.AutoFill;
import com.shanny.dto.ArticleDTO;
import com.shanny.entity.About;
import com.shanny.entity.Article;
import com.shanny.entity.Category;
import com.shanny.enums.AutoFillEnum;
import com.shanny.enums.CategoryEnum;
import org.apache.ibatis.annotations.Delete;
import org.apache.ibatis.annotations.Select;

import java.util.List;

public interface ArticleMapper {
    @Select("select * from shanny_blog.articles ORDER BY update_time DESC LIMIT 5")
    List<Article> getByRecent();

    @Select("select * from shanny_blog.articles where type = #{type}")
    List<Article> getByType(CategoryEnum.CategoryType type);

    @Select("select * from shanny_blog.articles where id = #{id}")
    Article getById(Long id);

    @AutoFill(AutoFillEnum.OperationType.INSERT)
    void insert_article(Article article);

    @AutoFill(AutoFillEnum.OperationType.UPDATE)
    void update_article(Article article);

    @Delete("delete from shanny_blog.articles where id = #{id}")
    void deleteById(Long id);

    @Select("select * from shanny_blog.articles ORDER BY views DESC LIMIT 5")
    List<Article> getByView();

    void update_article_views(Article article);

    @Select("SELECT * FROM shanny_blog.articles WHERE JSON_CONTAINS(tags, CAST(#{tagId} AS JSON))")
    List<Article> getByTag(Long tagId);
}

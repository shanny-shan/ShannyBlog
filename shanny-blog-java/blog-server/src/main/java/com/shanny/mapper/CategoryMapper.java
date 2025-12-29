package com.shanny.mapper;

import com.shanny.annotation.AutoFill;
import com.shanny.entity.Category;
import com.shanny.entity.User;
import com.shanny.entity.UserDetails;
import com.shanny.enums.AutoFillEnum.OperationType;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

public interface CategoryMapper {

    @Select("select * from shanny_blog.categories")
    List<Category> getAll();

    @Select("select * from shanny_blog.categories where id = #{id}")
    Category getById(Long id);

    Category updateById(Category category);

    void insert_category(Category category);
}

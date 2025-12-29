package com.shanny.mapper;

import com.shanny.annotation.AutoFill;
import com.shanny.entity.About;
import com.shanny.entity.Tag;
import com.shanny.enums.AutoFillEnum;
import org.apache.ibatis.annotations.Select;

import java.util.List;

public interface TagMapper {
    @Select("select * from shanny_blog.tags")
    List<Tag> getAll();

    @Select("select * from shanny_blog.tags where id = #{id}")
    Tag getById(Long id);

    void insert_tag(Tag tag);
}

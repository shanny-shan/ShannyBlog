package com.shanny.mapper;

import com.shanny.annotation.AutoFill;
import com.shanny.entity.About;
import com.shanny.enums.AutoFillEnum;
import org.apache.ibatis.annotations.Select;

import java.util.List;

public interface AboutMapper {
    @Select("select * from shanny_blog.abouts where is_active = #{isActive}")
    About getByShow(Boolean isActive);

    @AutoFill(AutoFillEnum.OperationType.INSERT)
    void insert_about(About about);

    @Select("select * from shanny_blog.abouts")
    List<About> getAll();
}

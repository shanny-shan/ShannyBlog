package com.shanny.mapper;

import com.shanny.annotation.AutoFill;
import com.shanny.entity.Article;
import com.shanny.entity.Tool;
import com.shanny.enums.AutoFillEnum;
import com.shanny.enums.CategoryEnum;
import org.apache.ibatis.annotations.Select;

import java.util.List;

public interface ToolMapper {
    @Select("select * from shanny_blog.tools")
    List<Tool> getAll();

    @AutoFill(AutoFillEnum.OperationType.INSERT)
    void insert_tool(Tool tool);
}

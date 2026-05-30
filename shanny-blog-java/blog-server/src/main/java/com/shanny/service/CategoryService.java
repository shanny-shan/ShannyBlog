package com.shanny.service;

import com.shanny.dto.CategoryDTO;
import com.shanny.dto.LoginDTO;
import com.shanny.dto.RegisterDTO;
import com.shanny.result.Result;
import com.shanny.vo.CategoryVO;
import com.shanny.vo.LoginVO;
import com.shanny.vo.UserInfoVO;

import java.util.List;

public interface CategoryService {
    Result<List<CategoryVO>> getCategories();

    Result<CategoryVO> addCategory(CategoryDTO categoryDTO);

    Result<CategoryVO> updateCategory(CategoryDTO categoryDTO);

    Result<String> deleteCategoryById(Long id);
}

package com.shanny.service.impl;

import com.shanny.constant.JwtClaimsConstant;
import com.shanny.context.BaseContext;
import com.shanny.dto.CategoryDTO;
import com.shanny.dto.LoginDTO;
import com.shanny.dto.RegisterDTO;
import com.shanny.entity.About;
import com.shanny.entity.Category;
import com.shanny.entity.User;
import com.shanny.entity.UserDetails;
import com.shanny.enums.UserEnum.UserSex;
import com.shanny.enums.UserEnum.UserStatus;
import com.shanny.mapper.CategoryMapper;
import com.shanny.mapper.UserMapper;
import com.shanny.properties.JwtProperties;
import com.shanny.result.Result;
import com.shanny.service.CategoryService;
import com.shanny.service.UserService;
import com.shanny.utils.JwtUtil;
import com.shanny.vo.AboutVO;
import com.shanny.vo.CategoryVO;
import com.shanny.vo.LoginVO;
import com.shanny.vo.UserInfoVO;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.util.DigestUtils;

import java.time.LocalDateTime;
import java.util.*;
import java.util.stream.Collectors;

import static com.shanny.constant.DefaultConstant.DEFAULT_INTRO;
import static com.shanny.constant.DefaultConstant.DEFAULT_UUID;
import static com.shanny.constant.ResultConstant.*;

@Service
public class CategoryServiceImpl implements CategoryService {
    private final CategoryMapper categoryMapper;

    @Autowired
    public CategoryServiceImpl(CategoryMapper categoryMapper) {
        this.categoryMapper = categoryMapper;
    }

    @Override
    public Result<List<CategoryVO>> getCategories() {
        List<Category> categories = categoryMapper.getAll();
        List<CategoryVO> categoryVOList = categories.stream().map(category -> {
            CategoryVO vo = new CategoryVO();
            BeanUtils.copyProperties(category, vo);
            return vo;
        }).toList();
        return Result.success(SELECT_SUCCESS, categoryVOList);
    }

    @Override
    public Result<CategoryVO> addCategory(CategoryDTO categoryDTO) {
        Category category = new Category();
        BeanUtils.copyProperties(categoryDTO, category);
        category.setName(categoryDTO.getName());
        category.setSort(categoryDTO.getSort());
        category.setNameEn(categoryDTO.getNameEn());
        category.setType(categoryDTO.getType());

        categoryMapper.insert_category(category);

        CategoryVO vo = new CategoryVO();
        BeanUtils.copyProperties(category, vo);

        return Result.success(INSERT_SUCCESS, vo);
    }

    @Override
    public Result<CategoryVO> updateCategory(CategoryDTO categoryDTO) {
        if(categoryDTO.getId() == null){
            return Result.error(UPDATE_FAIL);
        }

        Category category = new Category();
        BeanUtils.copyProperties(categoryDTO, category);
        categoryMapper.update_category(category);

        CategoryVO categoryVO = new CategoryVO();
        BeanUtils.copyProperties(category, categoryVO);

        return Result.success(UPDATE_SUCCESS, categoryVO);
    }

    @Override
    public Result<String> deleteCategoryById(Long id) {
        if(id == null){
            return Result.error(DELETE_FAIL);
        }
        categoryMapper.deleteById(id);
        return Result.success(DELETE_SUCCESS);
    }
}

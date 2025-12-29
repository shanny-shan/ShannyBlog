package com.shanny.service.impl;

import com.shanny.dto.AboutDTO;
import com.shanny.dto.CategoryDTO;
import com.shanny.entity.About;
import com.shanny.entity.Article;
import com.shanny.entity.Category;
import com.shanny.enums.AboutEnum;
import com.shanny.mapper.AboutMapper;
import com.shanny.mapper.CategoryMapper;
import com.shanny.result.Result;
import com.shanny.service.AboutService;
import com.shanny.service.CategoryService;
import com.shanny.vo.AboutVO;
import com.shanny.vo.ArticleVO;
import com.shanny.vo.CategoryVO;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

import static com.shanny.constant.ResultConstant.INSERT_SUCCESS;
import static com.shanny.constant.ResultConstant.SELECT_SUCCESS;

@Service
public class AboutServiceImpl implements AboutService {
    private final AboutMapper aboutMapper;

    @Autowired
    public AboutServiceImpl(AboutMapper aboutMapper) {
        this.aboutMapper = aboutMapper;
    }

    @Override
    public Result<List<AboutVO>> getAboutMe() {
        List<About> abouts = aboutMapper.getAll();
        List<AboutVO> aboutVOList = abouts.stream().map(about -> {
            AboutVO vo = new AboutVO();
            BeanUtils.copyProperties(about, vo);
            return vo;
        }).toList();
        return Result.success(SELECT_SUCCESS, aboutVOList);
    }

    @Override
    public Result<AboutVO> getAboutMeByShow() {
        About about = aboutMapper.getByShow(true);
        AboutVO aboutVO = new AboutVO();
        BeanUtils.copyProperties(about, aboutVO);
        return Result.success(SELECT_SUCCESS, aboutVO);
    }

    @Override
    public Result<AboutVO> addAbout(AboutDTO aboutDTO) {
        About about = new About();
        BeanUtils.copyProperties(aboutDTO, about);
        About old = aboutMapper.getByShow(true);
        if(old != null){
            about.setIsActive(false);
        }

        aboutMapper.insert_about(about);

        AboutVO aboutVO = new AboutVO();
        BeanUtils.copyProperties(about, aboutVO);

        return Result.success(INSERT_SUCCESS, aboutVO);
    }
}

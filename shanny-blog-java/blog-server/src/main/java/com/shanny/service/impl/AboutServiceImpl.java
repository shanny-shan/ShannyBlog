package com.shanny.service.impl;

import com.shanny.dto.AboutDTO;
import com.shanny.dto.CategoryDTO;
import com.shanny.dto.ToolDTO;
import com.shanny.entity.About;
import com.shanny.entity.Article;
import com.shanny.entity.Category;
import com.shanny.entity.Tool;
import com.shanny.enums.AboutEnum;
import com.shanny.mapper.AboutMapper;
import com.shanny.mapper.CategoryMapper;
import com.shanny.result.Result;
import com.shanny.service.AboutService;
import com.shanny.service.CategoryService;
import com.shanny.vo.AboutVO;
import com.shanny.vo.ArticleVO;
import com.shanny.vo.CategoryVO;
import com.shanny.vo.ToolVO;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

import static com.shanny.constant.ResultConstant.*;
import static com.shanny.constant.ResultConstant.DELETE_FAIL;
import static com.shanny.constant.ResultConstant.DELETE_SUCCESS;
import static com.shanny.constant.ResultConstant.UPDATE_SUCCESS;

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

        String src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
        int randomNum = (int) (Math.random() * 6) + 1;
        about.setAvatar(src + randomNum + ".jpg");

        aboutMapper.insert_about(about);

        AboutVO aboutVO = new AboutVO();
        BeanUtils.copyProperties(about, aboutVO);

        return Result.success(INSERT_SUCCESS, aboutVO);
    }

    @Override
    public Result<AboutVO> updateAbout(AboutDTO aboutDTO)  {
        if(aboutDTO.getId() == null){
            return Result.error(UPDATE_FAIL);
        }

        About about = new About();
        BeanUtils.copyProperties(aboutDTO, about);
        aboutMapper.update_about(about);

        AboutVO aboutVO = new AboutVO();
        BeanUtils.copyProperties(about, aboutVO);

        return Result.success(UPDATE_SUCCESS, aboutVO);
    }

    @Override
    public Result<String> deleteAboutById(Long id) {
        if(id == null){
            return Result.error(DELETE_FAIL);
        }
        aboutMapper.deleteById(id);
        return Result.success(DELETE_SUCCESS);
    }
}

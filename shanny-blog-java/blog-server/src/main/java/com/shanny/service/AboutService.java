package com.shanny.service;

import com.shanny.dto.AboutDTO;
import com.shanny.dto.CategoryDTO;
import com.shanny.result.Result;
import com.shanny.vo.AboutVO;
import com.shanny.vo.CategoryVO;

import java.util.List;

public interface AboutService {
    Result<List<AboutVO>> getAboutMe();

    Result<AboutVO> getAboutMeByShow();

    Result<AboutVO> addAbout(AboutDTO aboutDTO);

    Result<AboutVO> updateAbout(AboutDTO aboutDTO);

    Result<String> deleteAboutById(Long id);
}

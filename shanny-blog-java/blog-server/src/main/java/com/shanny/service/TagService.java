package com.shanny.service;

import com.shanny.dto.TagDTO;
import com.shanny.result.Result;
import com.shanny.vo.TagVO;

import java.util.List;

public interface TagService {
    Result<List<TagVO>> getTags();

    Result<TagVO> addTag(TagDTO tagDTO);

    Result<TagVO> getTagsById(Long id);
}

package com.shanny.service.impl;

import com.shanny.dto.TagDTO;
import com.shanny.entity.Article;
import com.shanny.entity.Tag;
import com.shanny.mapper.TagMapper;
import com.shanny.result.Result;
import com.shanny.service.TagService;
import com.shanny.vo.ArticleVO;
import com.shanny.vo.TagVO;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

import static com.shanny.constant.ResultConstant.INSERT_SUCCESS;
import static com.shanny.constant.ResultConstant.SELECT_SUCCESS;

@Service
public class TagServiceImpl implements TagService {
    private final TagMapper tagMapper;

    @Autowired
    public TagServiceImpl(TagMapper tagMapper) {
        this.tagMapper = tagMapper;
    }

    @Override
    public Result<List<TagVO>> getTags() {
        List<Tag> tags = tagMapper.getAll();
        List<TagVO> tagVOList = tags.stream().map(tag -> {
            TagVO vo = new TagVO();
            BeanUtils.copyProperties(tag, vo);
            return vo;
        }).toList();
        return Result.success(SELECT_SUCCESS, tagVOList);
    }

    @Override
    public Result<TagVO> getTagsById(Long id) {
        Tag tag = tagMapper.getById(id);
        TagVO vo = new TagVO();
        BeanUtils.copyProperties(tag, vo);
        return Result.success(SELECT_SUCCESS, vo);
    }

    @Override
    public Result<TagVO> addTag(TagDTO tagDTO) {
        Tag tag = new Tag();
        BeanUtils.copyProperties(tagDTO, tag);
        tagMapper.insert_tag(tag);
        TagVO vo = new TagVO();
        BeanUtils.copyProperties(tag, vo);
        return  Result.success(INSERT_SUCCESS, vo);
    }


}

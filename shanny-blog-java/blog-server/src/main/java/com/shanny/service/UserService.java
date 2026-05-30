package com.shanny.service;

import com.shanny.dto.RegisterDTO;
import com.shanny.dto.LoginDTO;
import com.shanny.dto.UserInfoDTO;
import com.shanny.result.Result;
import com.shanny.vo.LoginVO;
import com.shanny.vo.UserInfoVO;

import java.util.List;

public interface UserService {
    Result<String> save(RegisterDTO registerDTO);

    Result<LoginVO> login(LoginDTO loginDTO);

    Result<UserInfoVO> getUserInfo();

    Result<List<UserInfoVO>> getUsers();

    Result<UserInfoVO> updateUserInfo(UserInfoDTO userInfoDTO);
}

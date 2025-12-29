package com.shanny.service.impl;

import com.shanny.constant.JwtClaimsConstant;
import com.shanny.context.BaseContext;
import com.shanny.dto.RegisterDTO;
import com.shanny.dto.LoginDTO;
import com.shanny.entity.User;
import com.shanny.entity.UserDetails;
import com.shanny.enums.UserEnum.*;
import com.shanny.mapper.UserMapper;
import com.shanny.properties.JwtProperties;
import com.shanny.result.Result;
import com.shanny.service.UserService;
import com.shanny.utils.JwtUtil;
import com.shanny.vo.LoginVO;
import com.shanny.vo.UserInfoVO;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.util.DigestUtils;

import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

import static com.shanny.constant.DefaultConstant.DEFAULT_INTRO;
import static com.shanny.constant.DefaultConstant.DEFAULT_UUID;
import static com.shanny.constant.ResultConstant.*;

@Service
public class UserServiceImpl implements UserService {
    private final UserMapper userMapper;
    private final JwtProperties jwtProperties;

    @Autowired
    public UserServiceImpl(UserMapper userMapper, JwtProperties jwtProperties) {
        this.userMapper = userMapper;
        this.jwtProperties = jwtProperties;
    }

    @Override
    @Transactional
    public Result<String> save(RegisterDTO registerDTO) {
        User userByUserId = userMapper.getByUserId(registerDTO.getUserId());
        User userByMobile = userMapper.getByMobile(registerDTO.getMobile());
        if (userByUserId != null) {
            return Result.error(USER_ID_EXISTED);
        } else if (userByMobile != null) {
            return Result.error(MOBILE_EXISTED);
        } else {
            User user = new User();
            BeanUtils.copyProperties(registerDTO, user);
            String uuid = DEFAULT_UUID;
            String password = DigestUtils.md5DigestAsHex(registerDTO.getPassword().getBytes());
            user.setUuid(uuid);
            user.setPassword(password);
            user.setStatus(UserStatus.ACTIVE);
            user.setType(UserType.USER);
            user.setLastLoginTime(LocalDateTime.now());
            userMapper.insert_user(user);

            UserDetails userDetails = new UserDetails();
            userDetails.setUuid(uuid);
            userDetails.setSex(UserSex.UNKNOWN);
            userMapper.insert_user_detail(userDetails);
            return Result.success(REGISTER_SUCCESS);
        }
    }

    @Override
    public Result<LoginVO> login(LoginDTO loginDTO) {
        String userId = loginDTO.getUserId();
        String password = loginDTO.getPassword();

        User user = userMapper.getByUserId(userId);

        if (user == null) {
            return Result.error(ACCOUNT_NOT_FOUND);
        }

        password = DigestUtils.md5DigestAsHex(password.getBytes());
        if (!password.equals(user.getPassword())) {
            return Result.error(PASSWORD_ERROR);
        }

        //登录成功后，生成jwt令牌
        Map<String, Object> claims = new HashMap<>();
        claims.put(JwtClaimsConstant.USER_ID, userId);
        String token = JwtUtil.createJWT(
                jwtProperties.getUserSecretKey(),
                jwtProperties.getUserTtl(),
                claims);
        LoginVO loginVO = LoginVO
                .builder()
                .userId(userId)
                .token(token)
                .build();

        user.setLastLoginTime(LocalDateTime.now());
        userMapper.update_user(user);

        return Result.success(LOGIN_SUCCESS, loginVO);
    }

    @Override
    public Result<UserInfoVO> getUserInfo() {
        String userId = BaseContext.getCurrentId();
        if(userId != null){
            User user = userMapper.getByUserId(userId);
            UserDetails userDetails = userMapper.getDetailByUuid(user.getUuid());
            UserInfoVO userInfoVO = UserInfoVO
                    .builder()
                    .uuid(user.getUuid())
                    .userId(userId)
                    .createTime(user.getCreateTime())
                    .lastLoginTime(user.getLastLoginTime())
                    .mobile(user.getMobile())
                    .status(user.getStatus())
                    .type(user.getType())
                    .updateTime(user.getUpdateTime())
                    .userDetails(userDetails)
                    .build();
            return Result.success(LOGIN_SUCCESS, userInfoVO);
        }
        else{
            return Result.error(USERINFO_IS_NULL);
        }
    }

    @Override
    public Result<List<UserInfoVO>> getUsers() {
        String userId = BaseContext.getCurrentId();
        if(userId != null){
            List<User> users = userMapper.getUsers();
            List<UserDetails> details = userMapper.getUserDetails();
            Map<String, UserDetails> detailsMap = details.stream()
                    .collect(Collectors.toMap(UserDetails::getUuid, d -> d));
            List<UserInfoVO> userInfos = users.stream().map(u -> {
                UserInfoVO vo = new UserInfoVO();
                vo.setUuid(u.getUuid());
                vo.setUserId(u.getUserId());
                vo.setMobile(u.getMobile());
                vo.setStatus(u.getStatus());
                vo.setType(u.getType());
                vo.setCreateTime(u.getCreateTime());
                vo.setUpdateTime(u.getUpdateTime());
                vo.setLastLoginTime(u.getLastLoginTime());
                vo.setUserDetails(detailsMap.get(u.getUuid()));
                return vo;
            }).collect(Collectors.toList());
            return Result.success(SELECT_SUCCESS, userInfos);
        }
        else{
            return Result.error(LOGIN_ERROR);
        }
    }
}

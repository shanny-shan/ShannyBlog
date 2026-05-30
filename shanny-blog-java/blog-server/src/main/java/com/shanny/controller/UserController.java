package com.shanny.controller;

import com.shanny.dto.RegisterDTO;
import com.shanny.dto.LoginDTO;
import com.shanny.dto.UserInfoDTO;
import com.shanny.result.Result;
import com.shanny.service.UserService;
import com.shanny.vo.LoginVO;
import com.shanny.vo.UserInfoVO;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController()
@RequestMapping("/account")
@Tag(name = "账户相关接口")
@Slf4j
public class UserController {
    private final UserService userService;

    @Autowired
    public UserController(UserService userService) {
        this.userService = userService;
    }

    @PostMapping("/register")
    @Operation(summary = "用户注册")
    public Result<String> register(@RequestBody RegisterDTO registerDTO) {
        try {
            return userService.save(registerDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/login")
    @Operation(summary = "用户登录")
    public Result<LoginVO> login(@RequestBody LoginDTO loginDTO) {
        try {
            return userService.login(loginDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }

    @GetMapping("/userinfo")
    @Operation(summary = "获取用户基础信息")
    public Result<UserInfoVO> getUserInfo(){
        try{
            return userService.getUserInfo();
        }catch (Exception e){
            return Result.error(e.getMessage());
        }
    }

    @GetMapping("/users")
    @Operation(summary = "获取所有用户数据")
    public Result<List<UserInfoVO>> getUsers(){
        try{
            return userService.getUsers();
        }catch (Exception e){
            return Result.error(e.getMessage());
        }
    }

    @PostMapping("/update")
    @Operation(summary = "用户更新")
    public Result<UserInfoVO> UpdateUserInfo(@RequestBody UserInfoDTO userInfoDTO) {
        try {
            return userService.updateUserInfo(userInfoDTO);
        } catch (Exception e) {
            return Result.error(e.getMessage());
        }
    }
}

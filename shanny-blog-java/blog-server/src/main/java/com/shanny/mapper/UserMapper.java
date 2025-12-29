package com.shanny.mapper;
import com.shanny.annotation.AutoFill;
import com.shanny.entity.User;
import com.shanny.entity.UserDetails;
import com.shanny.enums.AutoFillEnum.*;
import com.shanny.vo.UserInfoVO;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;

import java.util.List;

public interface UserMapper {
    @AutoFill(OperationType.INSERT)
    void insert_user(User user);

    @AutoFill(OperationType.INSERT)
    void insert_user_detail(UserDetails userDetails);

    @Select("select * from shanny_blog.users where user_id = #{userId}")
    User getByUserId(String userId);

    @Select("select * from shanny_blog.users where mobile = #{mobile}")
    User getByMobile(String mobile);

    @Select("select * from shanny_blog.user_details where uuid = #{uuid}")
    UserDetails getDetailByUuid(String uuid);

    @AutoFill(OperationType.UPDATE)
    void update_user(User user);

    @AutoFill(OperationType.UPDATE)
    void update_user_detail(UserDetails userDetails);

    @Select("select * from shanny_blog.users")
    List<User> getUsers();

    @Select("select * from shanny_blog.user_details")
    List<UserDetails> getUserDetails();
}

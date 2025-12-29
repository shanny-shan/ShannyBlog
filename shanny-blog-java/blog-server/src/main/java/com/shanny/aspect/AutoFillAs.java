package com.shanny.aspect;

import com.shanny.annotation.AutoFill;
import com.shanny.constant.AutoFillConstant;
import com.shanny.enums.AutoFillEnum.*;
import lombok.extern.slf4j.Slf4j;
import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Before;
import org.aspectj.lang.annotation.Pointcut;
import org.aspectj.lang.reflect.MethodSignature;
import org.springframework.stereotype.Component;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.time.LocalDateTime;

/**
 * 公共字段自动填充
 */
@Aspect
@Component
@Slf4j
public class AutoFillAs {
    @Pointcut("execution(* com.shanny..*.*(..)) && @annotation(com.shanny.annotation.AutoFill)")
    public void autoFillPointcut() {}

    /**
     * 公共字段处理
     */
    @Before("autoFillPointcut()")
    public void autoFill(JoinPoint joinPoint) throws NoSuchMethodException, InvocationTargetException, IllegalAccessException {
        log.info("开始处理公共字段...");

        // 获取到当前被拦截的方法上的数据库操作类型
        MethodSignature signature = (MethodSignature) joinPoint.getSignature();
        AutoFill autoFillTime = signature.getMethod().getAnnotation(AutoFill.class);
        OperationType operationType = autoFillTime.value();

        // 获取别拦截方法的参数（实体对象）
        Object[] args = joinPoint.getArgs();
        if (args == null || args.length == 0) {
            return;
        }
        Object entity = args[0];
        LocalDateTime now = LocalDateTime.now();
//        Long currentId = Long.valueOf(BaseContext.getCurrentId());
        // 通过反射，为公共属性赋值
        if (operationType == OperationType.INSERT){
            Method seCreateTime = entity.getClass().getDeclaredMethod(AutoFillConstant.SET_CREATE_TIME, LocalDateTime.class);
            seCreateTime.invoke(entity, now);
        }
        Method setUpdateTime = entity.getClass().getDeclaredMethod(AutoFillConstant.SET_UPDATE_TIME, LocalDateTime.class);
        setUpdateTime.invoke(entity, now);
    }
}

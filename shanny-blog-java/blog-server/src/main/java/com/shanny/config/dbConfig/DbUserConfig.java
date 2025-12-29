package com.shanny.config.dbConfig;

import org.apache.ibatis.session.SqlSessionFactory;
import org.mybatis.spring.SqlSessionFactoryBean;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.boot.orm.jpa.EntityManagerFactoryBuilder;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Primary;
import org.springframework.core.io.support.PathMatchingResourcePatternResolver;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;
import org.springframework.jdbc.datasource.DataSourceTransactionManager;
import org.springframework.orm.jpa.LocalContainerEntityManagerFactoryBean;
import org.springframework.transaction.annotation.EnableTransactionManagement;

import javax.sql.DataSource;

@Configuration
@EnableTransactionManagement
@EnableJpaRepositories(
        basePackages = "com.shanny.repository", // 指定 db-user 的 repository 包
        entityManagerFactoryRef = "dbUserEntityManagerFactory",
        transactionManagerRef = "dbUserTransactionManager"
)
@MapperScan(
        basePackages = "com.shanny.mapper",
        sqlSessionFactoryRef = "dbUserSqlSessionFactory")
public class DbUserConfig {
    @Primary
    @Bean(name = "dbUserEntityManagerFactory")
    public LocalContainerEntityManagerFactoryBean entityManagerFactory(
            EntityManagerFactoryBuilder builder,
            @Qualifier("dbUserDataSource") DataSource dataSource) {
        return builder
                .dataSource(dataSource)
                .packages("com.shanny.entity") // 指定 db-user 实体类的包
                .persistenceUnit("User")
                .build();
    }
    @Primary
    @Bean(name = "dbUserSqlSessionFactory")
    public SqlSessionFactory sqlSessionFactory(
            @Qualifier("dbUserDataSource") DataSource dataSource)
            throws Exception {
        SqlSessionFactoryBean factoryBean = new SqlSessionFactoryBean();
        factoryBean.setDataSource(dataSource);
        factoryBean.setTypeHandlersPackage("com.shanny.handler");
        factoryBean.setMapperLocations(new PathMatchingResourcePatternResolver()
                .getResources("classpath:/mapper/*.xml"));
        factoryBean.setTypeAliasesPackage("com.shanny.entity");

        // 开启驼峰命名
        org.apache.ibatis.session.Configuration configuration = new org.apache.ibatis.session.Configuration();
        configuration.setMapUnderscoreToCamelCase(true);
        factoryBean.setConfiguration(configuration);

        return factoryBean.getObject();
    }
    @Primary
    @Bean(name = "dbUserTransactionManager")
    public DataSourceTransactionManager transactionManager(
            @Qualifier("dbUserDataSource") DataSource dataSource) {
        return new DataSourceTransactionManager(dataSource);
    }
}

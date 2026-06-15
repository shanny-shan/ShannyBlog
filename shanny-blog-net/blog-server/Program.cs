/*
dotnet tool install --global dotnet-ef ：EF命令
Add-Migration InitCreateTable
Update-Database
http://localhost:8080/swagger
*/
using blog_common.Config;
using blog_common.Constant;
using blog_common.Result;
using blog_db;
using blog_server.Annotatin;
using blog_server.Interceptor;
using blog_server.Service;
using blog_server.Service.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 数据库上下文
builder.Services.AddDbContext<_DbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BlogDb"),
        b => b.MigrationsAssembly("blog-db")
    );
});

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));
var jwtConfig = new JwtConfig();
builder.Configuration.GetSection("Jwt").Bind(jwtConfig);
builder.Services.AddSingleton(jwtConfig);

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("App"));
var appConfig = new AppConfig();
builder.Configuration.GetSection("App").Bind(appConfig);
builder.Services.AddSingleton(appConfig);

// JWT
var jwtSection = builder.Configuration.GetSection("Jwt");
string userSecretKey = jwtSection["UserSecretKey"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userSecretKey))
        };
        opt.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                var res = Result<string>.Error(ResultMsg.LoginError);
                return context.Response.WriteAsJsonAsync(res);
            }
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddScoped<JwtTokenUserInterceptor>();
builder.Services.AddScoped<WebMvcConfig>();

builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IToolService, ToolService>();
builder.Services.AddScoped<IUserService, UserService>();

// 跨域
string corsPolicyName = "ShannyCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.WithOrigins(
                "https://www.shanny.work",
                "https://shanny.work",
                "http://localhost:5174",
                "http://localhost:5173"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// 控制器 + 全局异常过滤器
builder.Services.AddControllers(options =>
{
    var sp = builder.Services.BuildServiceProvider();
    var mvcConfig = sp.GetRequiredService<WebMvcConfig>();
    mvcConfig.AddInterceptors(options);
    options.Filters.Add<GlobalExceptionFilter>();
    options.Filters.Add<ModelValidateFilter>();
})
.AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assembly = Assembly.GetExecutingAssembly();
    var projectVer = assembly.GetName().Version?.ToString(3);
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShannyBlog",
        Description = "API 文档",
        Version = $"{projectVer}",
        Contact = new OpenApiContact
        {
            Name = "Shanny",
            Email = "wangshanshanwork@gmail.com",
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://springdoc.org")
        }
    });

    c.SwaggerDoc("user", new OpenApiInfo
    {
        Title = "用户管理",
        Version = $"{projectVer}"
    });

    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (docName == "v1") return true;
        if (docName == "user" && apiDesc.RelativePath != null)
            return apiDesc.RelativePath.StartsWith("account/");
        return false;
    });

    c.DocumentFilter<ExternalDocsFilter>();

    // JWT Bearer 认证框
    var bearerScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "填写格式：Bearer 你的Token"
    };
    c.AddSecurityDefinition("Bearer", bearerScheme);

    c.AddSecurityRequirement(document => {
        var schemeRef = new OpenApiSecuritySchemeReference("Bearer", document);
        return new OpenApiSecurityRequirement { [schemeRef] = [] };
    });
});

var app = builder.Build();

// 自动迁移数据库
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<_DbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        dbContext.Database.Migrate();
        logger.LogInformation("数据库迁移执行完成");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "数据库迁移失败");
    }
}

// 中间件顺序
app.UseCors(corsPolicyName);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "全部接口 v1");
        c.SwaggerEndpoint("/swagger/user/swagger.json", "用户模块 user");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:8080");

public class ExternalDocsFilter : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, Swashbuckle.AspNetCore.SwaggerGen.DocumentFilterContext context)
    {
        swaggerDoc.ExternalDocs = new OpenApiExternalDocs
        {
            Description = "外部参考文档",
            Url = new Uri("https://springshop.wiki.github.org/docs")
        };
    }
}

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var errResult = Result<string>.Error(context.Exception.Message);
        context.Result = new ObjectResult(errResult)
        {
            StatusCode = 200
        };
        context.ExceptionHandled = true;
    }
}
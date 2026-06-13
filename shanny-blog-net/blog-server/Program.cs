using blog_common.Config;
using blog_server.Annotatin;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

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
builder.Services.AddShannyCors();

builder.Services.AddScoped<JwtTokenUserInterceptor>();
builder.Services.AddScoped<WebMvcConfig>();
builder.Services.AddScoped<JwtConfig>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShannyBlog",
        Description = "workplace API 文档",
        Version = "v1.0.0",
        Contact = new OpenApiContact
        {
            Name = "Shanny",
            Email = "wangshanshanwork@gamil.com",
            Url = new Uri("") 
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
        Version = "v1.0.0"
    });

    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (docName == "v1") return true;
        if (docName == "user")
            return apiDesc.RelativePath != null && apiDesc.RelativePath.StartsWith("user/");
        return false;
    });

    c.DocumentFilter<ExternalDocsFilter>();
});

builder.Services.AddControllers(options =>
{
    var config = builder.Services.BuildServiceProvider().GetRequiredService<WebMvcConfig>();
    config.AddInterceptors(options);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(corsPolicyName);
app.UseShannyCors();

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "全部接口");
    opt.SwaggerEndpoint("/swagger/user/swagger.json", "用户管理");
    opt.RoutePrefix = string.Empty;
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:8080");

public class ExternalDocsFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.ExternalDocs = new OpenApiExternalDocs
        {
            Description = "外部文档",
            Url = new Uri("https://springshop.wiki.github.org/docs")
        };
    }
}

namespace blog_server.Annotatin
{
    public static class CorsConfig
    {
        public static void AddShannyCors(this IServiceCollection services)
        {
            const string policyName = "ShannyCors";
            services.AddCors(opt =>
            {
                opt.AddPolicy(policyName, p =>
                {
                    p.WithOrigins(
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
        }

        public static void UseShannyCors(this WebApplication app)
        {
            app.UseCors("ShannyCors");
        }
    }
}

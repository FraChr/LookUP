namespace API.Extensions.CorsExtensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost8080",
                policy =>
                {
                    policy.WithOrigins("http://localhost:8080", "http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        return services;
    }

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors("AllowLocalhost8080");
        return app;
    }
}


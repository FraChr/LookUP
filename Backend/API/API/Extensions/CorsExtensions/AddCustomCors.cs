namespace API.Extensions.CorsExtensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("MyAllowSpecificOrigins",
                policy =>
                { policy.WithOrigins("http://localhost:8080", "http://localhost:5173", "http://localhost:1433")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        return services;
    }

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors("MyAllowSpecificOrigins");
        return app;
    }
}


using API.Model;
using API.Services;
using API.Services.Interfaces;
using API.Storage;
using Microsoft.AspNetCore.Http.Features;

namespace API.Extensions.ServiceExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ICrudService<Item>, ItemService>();
        services.AddScoped<ICrudService<Location>, LocationService>();
        services.AddScoped<ICrudService<User>, UserService>();
        services.AddScoped<IValidate, Validate>();
        services.AddScoped<EndpointMapperService>();
        services.AddScoped<ConnectionBuilder>();
        return services;
    }
}
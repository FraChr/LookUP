using API.Model;
using API.Model.Items;
using API.Model.Location;
using API.Model.User;
using API.Services;
using API.Services.Interfaces;
using API.Storage;
using Microsoft.AspNetCore.Http.Features;

namespace API.Extensions.ServiceExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ICrudService<Item, ItemDto, ItemViewModel>, ItemService>();
        services.AddScoped<ICrudService<Location, LocationDto, LocationViewModel>, LocationService>();
        services.AddScoped<ICrudService<User, UserDto, UserViewModel>, UserService>();
        services.AddScoped<IValidate, Validate>();
        services.AddScoped<EndpointMapperService>();

        services.AddScoped<ConnectionBuilder>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
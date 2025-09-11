using API.Endpoints;
using API.Model.User;
using API.Services.Interfaces;

namespace API.Services;

public class EndpointMapperService : IEndpointMapper
{
    public void MapEndpoints<TEntity, TDto, TViewModel>(WebApplication app, string route)
    {
        app.MapGet($"/{route}", EndpointHandlers.HandleGetAll<TEntity, TDto, TViewModel>).RequireAuthorization();

        app.MapGet($$"""/{{route}}/{id:int}""", EndpointHandlers.HandleGetById<TEntity, TDto, TViewModel>).RequireAuthorization();

        app.MapPost($"/{route}", EndpointHandlers.HandleCreate<TEntity, TDto, TViewModel>).RequireAuthorization();

        app.MapPut($$"""/{{route}}/{id:int}""", EndpointHandlers.HandleUpdate<TEntity, TDto, TViewModel>).RequireAuthorization();

        app.MapDelete($$"""/{{route}}/{id:int}""", EndpointHandlers.HandleDelete<TEntity, TDto, TViewModel>).RequireAuthorization();
    }

    public void MapAuthEndpoints(WebApplication app)
    {
        app.MapPost("/auth/login", EndpointHandlers.HandleLogin);
    }
}
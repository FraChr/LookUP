using API.Services.Interfaces;

namespace API.Services;

public class EndpointMapperService : IEndpointMapper
{
    public void MapEndpoints<T>(WebApplication app, string route)
    {
        app.MapGet($"/{route}", EndpointHandlers.HandleGetAll<T>);

        app.MapGet($$"""/{{route}}/{id:int}""", EndpointHandlers.HandleGetById<T>);

        app.MapPost($"/{route}", EndpointHandlers.HandleCreate<T>);

        app.MapPost($"/{route}", EndpointHandlers.HandleCreate<T>);

        app.MapPut($$"""/{{route}}/{id:int}""", EndpointHandlers.HandleUpdate<T>);

        app.MapDelete($$"""/{{route}}/{id:int}""", EndpointHandlers.HandleDelete<T>);
    }
}
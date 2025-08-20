namespace API.Services.Interfaces;

public interface IEndpointMapper
{
    void MapEndpoints<T>(WebApplication app, string route);
}
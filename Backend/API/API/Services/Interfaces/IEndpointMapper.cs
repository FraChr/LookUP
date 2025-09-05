namespace API.Services.Interfaces;

public interface IEndpointMapper
{
    void MapEndpoints<TEntity, TDto, TViewModel>(WebApplication app, string route);
}
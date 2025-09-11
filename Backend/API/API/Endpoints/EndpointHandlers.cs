using API.Model.User;
using API.Services.Interfaces;

namespace API.Endpoints;

public static class EndpointHandlers
{
    public static async Task<IResult> HandleGetAll<TEntity, TDto, TViewModel>(
        string? searchTerm,
        int? limit,
        int? page,
        ICrudService<TEntity, TDto, TViewModel> storage)
    {
        try
        {
            Console.WriteLine($"========================\nLimit: {limit}, Page: {page}");
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var result = await storage.Search(searchTerm, limit, page);
                return Results.Ok(result);
            }

            var all = await storage.GetAll(limit, page);
            return Results.Ok(all);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    public static async Task<IResult> HandleGetById<TEntity, TDto, TViewModel>(int id,
        ICrudService<TEntity, TDto, TViewModel> storage)
    {
        var result = await storage.GetById(id);
        return Results.Ok(result);
    }

    public static async Task<IResult> HandleCreate<TEntity, TDto, TViewModel>(TDto createdItem,
        ICrudService<TEntity, TDto, TViewModel> storage)
    {
        try
        {
            await storage.Create(createdItem);
            return Results.Ok(storage);
        }
        catch (Exception e)
        {
            return Results.BadRequest(new
            {
                message = e.Message,
            });
        }
    }

    public static async Task<IResult> HandleUpdate<TEntity, TDto, TViewModel>(int id, TDto modifiedItem,
        ICrudService<TEntity, TDto, TViewModel> storage)
    {
        try
        {
            var updatedItem = await storage.Update(modifiedItem, id);
            return Results.Ok(updatedItem);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: 400);
        }
    }

    public static async Task<IResult> HandleDelete<TEntity, TDto, TViewModel>(int id,
        ICrudService<TEntity, TDto, TViewModel> storage)
    {
        try
        {
            await storage.Delete(id);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: 500);
        }
    }

    public static async Task<IResult> HandleLogin(LoginDto loginDto, IAuthService authService)
    {
        try
        {
            var token = await authService.Login(loginDto);
            Console.WriteLine($"Token: {token}");
            return Results.Ok(new {token});
        }
        catch(Exception e)
        {
            return Results.Unauthorized();
        }
    }
}
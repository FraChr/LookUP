using API.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8080",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080", "http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowLocalhost8080");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var itemStorage = new ItemStorage();

app.MapGet("/storage", () =>
{
    return itemStorage.GetItems();
});

app.MapGet("/location", () =>
{
    return itemStorage.getLocations();
});


app.MapDelete("/storage/{id}", (int id) =>
{
    itemStorage.DeleteItem(id);
    return Results.NoContent();
});




app.Run();


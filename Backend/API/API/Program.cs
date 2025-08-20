using API.Model;
using API.Services;
using API.Services.Interfaces;
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

builder.Services.AddRazorPages();

builder.Services.AddScoped<ICrudService<Items>, ItemService>();
builder.Services.AddScoped<ICrudService<Location>, LocationService>();

var app = builder.Build();

app.UseCors("AllowLocalhost8080");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var storageEndpoints = new EndpointMapperService();
storageEndpoints.MapEndpoints<Items>(app, "storage");
storageEndpoints.MapEndpoints<Location>(app, "location");


app.Run();


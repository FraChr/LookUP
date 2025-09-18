using API;
using API.Data;
using API.Extensions;
using API.Extensions.AuthExtensions;
using API.Extensions.CorsExtensions;
using API.Extensions.ServiceExtensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionBuilder = serviceProvider.GetRequiredService<ConnectionBuilder>();
    options.UseSqlServer(connectionBuilder.GetConnectionString());
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCustomCors();
builder.Services.AddAppServices();
builder.Services.AddJwtAuthentication();


var app = builder.Build();
app.UseHttpsRedirection();

app.UseRouting();
app.UseCustomCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//Configure the HTTP request pipeline.
 // if (app.Environment.IsDevelopment())
 // {
 //     app.MapOpenApi();
 //
 // }

app.MapStorageEndpoints();

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

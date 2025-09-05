using System.Text;
using API;
using API.Data;
using API.Extensions;
using API.Extensions.CorsExtensions;
using API.Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// var connectionBuilder = new ConnectionBuilder(configuration);

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(connectionBuilder.GetConnectionString()));

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionBuilder = serviceProvider.GetRequiredService<ConnectionBuilder>();
    options.UseSqlServer(connectionBuilder.GetConnectionString());
});
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCustomCors();
builder.Services.AddAppServices();


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

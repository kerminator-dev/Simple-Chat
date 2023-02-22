using ChatAPI.Models;
using ChatAPI.Services.Interfaces;
using ChatAPI.Services.Implementation;
using ChatAPI.Extensions;
using ChatAPI.Utils;
using ChatAPI.Entities;
using ChatAPI.Hubs;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Cors.Infrastructure;
using ChatAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ���
builder.Services.AddSingleton<ICache<string, RefreshToken>, InMemoryRefreshTokenRepository>();
builder.Services.AddSingleton<ICache<string, User>, InMemoryUserRepository>();

builder.Services.AddSingleton<JwtUtils>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSignalR().AddJsonProtocol();

builder.Services.AddScoped<ChatHub>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.UseCors(builder =>
{
    builder.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin();
});
app.MapHub<ChatHub>("/chathub", options =>
{
    options.Transports = HttpTransportType.WebSockets;
    options.TransportMaxBufferSize = 32;
    options.ApplicationMaxBufferSize = 32;
});
app.UseMiddleware<JwtMiddleware>();


app.Run();

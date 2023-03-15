using Chat.WebAPI.Services.Implementation;
using ChatAPI.Extensions;
using ChatAPI.Hubs;
using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCachedRepositories();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessagingService, MessagingService>();
builder.Services.AddSignalR().AddJsonProtocol();
builder.Services.AddScoped<ChatHub>();
builder.Services.AddCustomRateLimit(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("CorsPolicy");
app.MapHub<ChatHub>("/Hub", options =>
{
    options.TransportMaxBufferSize = 32;
    options.ApplicationMaxBufferSize = 32;
    options.Transports = HttpTransportType.ServerSentEvents;
});

app.Run();

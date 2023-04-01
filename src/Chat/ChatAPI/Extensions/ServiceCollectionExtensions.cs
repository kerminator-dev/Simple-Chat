using Chat.WebAPI.Services.Implementation;
using ChatAPI.Data;
using ChatAPI.Entities;
using ChatAPI.Mappings;
using ChatAPI.Models;
using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

namespace ChatAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfig = new AuthenticationConfiguration();

            configuration.Bind("Authehtication", authConfig);
            services.AddSingleton<AuthenticationConfiguration>(authConfig);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authConfig.Issuer,
                    ValidAudience = authConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(authConfig.AccessTokenSecret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if (string.IsNullOrEmpty(accessToken) == false)
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
            

             return services;
        }

        public static IServiceCollection AddCustomRateLimit(this IServiceCollection services, IConfiguration configuration)
        {
            var rateLimitConfig = new RateLimitConfiguration();
            configuration.Bind("RateLimit", rateLimitConfig);

            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = false,
                            PermitLimit = rateLimitConfig.PermitLimit,
                            QueueLimit = rateLimitConfig.QueueLimit,
                            Window = TimeSpan.FromSeconds(rateLimitConfig.WindowSeconds)
                        }));
            });

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlite(configuration.GetConnectionString("SQLite"));
            }); 


            return services;
        }

        public static IServiceCollection AddCachedRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ICache<string, RefreshToken>, CachedRefreshTokenRepository>();
            services.AddSingleton<ICache<string, User>, CachedUserRepository>();
            services.AddSingleton<CachedUserConnectionMapper<string>>();

            return services;
        }
    }
}

﻿using ChatAPI.Services.Implementation;
using ChatAPI.Services.Interfaces;
using ChatAPI.Utils;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ChatAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, JwtUtils jwtUtils)
        {
            try
            {
                string? token = context.Request.Headers["Authorization"].FirstOrDefault();

                if (!String.IsNullOrEmpty(token))
                {
                    token = token.Replace("Bearer ", "");

                    if (jwtUtils.ValidateToken(token))
                    {
                        var username = context.User.Claims
                               .First(i => i.Type == ClaimTypes.NameIdentifier).Value;

                        if (!string.IsNullOrEmpty(username))
                        {
                            context.Items["User"] = await userService.GetUserByUsername(username);
                        }
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                await _next(context);
            }
        }
    }
}

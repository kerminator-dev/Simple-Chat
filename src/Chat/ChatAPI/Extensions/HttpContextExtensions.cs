using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace ChatAPI.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool TryGetToken(this HttpContext context, out string token, string claimType = ClaimTypes.NameIdentifier)
        {
            token = context.Request.Headers[HeaderNames.Authorization].ToString();

            return String.IsNullOrEmpty(context.Items["token"] as String);
        }
    }
}

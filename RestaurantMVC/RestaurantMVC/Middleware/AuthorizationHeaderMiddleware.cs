using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RestaurantMVC.Middleware
{
    public class AuthorizationHeaderMiddleware
    {
        RequestDelegate next;

        public AuthorizationHeaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Cookies["Authorization"];

            context.Request.Headers.Add("Authorization", $"Bearer {token}");

            await next(context);
        }
    }
}

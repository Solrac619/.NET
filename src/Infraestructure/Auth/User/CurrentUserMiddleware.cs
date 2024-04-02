using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Auth.User
{
    public class CurrentUserMiddleware : IMiddleware
    {
        private readonly ICurrentUserInitializerService _currentUserInitializer;

        public CurrentUserMiddleware(ICurrentUserInitializerService currentUserInitializer) =>
            _currentUserInitializer = currentUserInitializer;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _currentUserInitializer.SetCurrentUser(context.User);

            await next(context);
        }
    }
}
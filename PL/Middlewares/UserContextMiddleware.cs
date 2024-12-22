using Entities.Context.Abstract;

namespace PL.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserContext userContext)
        {
            var user = context.User;
            
            var userIdClaim = user.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                userContext.UserId = userId;
            }
            else
            {
                Console.WriteLine("UserId claim not found or invalid.");
            }

            await _next(context);
        }
    }
}

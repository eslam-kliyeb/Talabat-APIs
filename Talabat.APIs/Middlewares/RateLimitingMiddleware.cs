namespace Talabat.APIs.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Dictionary<string, (DateTime FirstRequestTime, int RequestCount)> _clients = new();
        private const int LIMIT = 5; // Max requests per client
        private static readonly TimeSpan TIME_WINDOW = TimeSpan.FromMinutes(1);
        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(clientIp))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Unable to determine client IP.");
                return;
            }

            lock (_clients)
            {
                if (_clients.ContainsKey(clientIp))
                {
                    var (firstRequestTime, requestCount) = _clients[clientIp];

                    if (DateTime.UtcNow - firstRequestTime < TIME_WINDOW)
                    {
                        if (requestCount >= LIMIT) // Block the request if the limit is exceeded
                        {
                            context.Response.StatusCode = 429; // Too Many Requests
                            context.Response.Headers.Add("Retry-After", TIME_WINDOW.TotalSeconds.ToString());
                            context.Response.ContentType = "text/plain";
                            context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                            return;
                        }
                        else _clients[clientIp] = (firstRequestTime, requestCount + 1);
                    }
                    else _clients[clientIp] = (DateTime.UtcNow, 1);
                }
                else _clients[clientIp] = (DateTime.UtcNow, 1);
            }
            await _next(context);
        }
    }
}

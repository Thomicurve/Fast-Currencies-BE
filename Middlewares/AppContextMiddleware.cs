namespace fast_currencies_be;

public class AppContextMiddleware
{
    private readonly RequestDelegate _next;
    public AppContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, FastCurrenciesAppContext appContext)
    {
        // Accede al usuario autenticado desde el contexto de HttpContext
        var userIdClaim = context.User.FindFirst("userId");

        if(userIdClaim != null) {
            appContext.UserId = int.Parse(userIdClaim.Value);
        }

        await _next(context);
    }
}

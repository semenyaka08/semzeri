using Semzeri.BusinessLogic.Exceptions;

namespace Semzeri.API.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException exception)
        {
            context.Response.StatusCode = 404;

            await context.Response.WriteAsync(exception.Message);
        }
        catch (UrlAlreadyShortenedException exception)
        {
            context.Response.StatusCode = 400;

            await context.Response.WriteAsync(exception.Message);
        }
        catch (ForbiddenException exception)
        {
            context.Response.StatusCode = 403;
            
            await context.Response.WriteAsync(exception.Message);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
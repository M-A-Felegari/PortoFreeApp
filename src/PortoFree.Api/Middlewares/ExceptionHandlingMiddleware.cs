using System.Net;
using System.Text.Json;
using FluentValidation;
using PortoFree.Application.Exceptions;
using PortoFree.Application.Interfaces.Logging;

namespace PortoFree.Api.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly IAppLogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(IAppLogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("validation exception : {@Exception}", ex);

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var response = new
            {
                errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
            };

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (ForbiddenException ex)
        {
            _logger.LogWarning("forbidden exception : {@Exception}", ex);

            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
                { message = ex.Message }); //todo: make this responses more flexible
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning("not found exception : {@Exception}", ex);
            
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
                { message = ex.Message }); //todo: make this responses more flexible
            
        }
        catch (Exception e)
        {
            _logger.LogError("Exception occured during request handling, error is : {@error}", e);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("something went wrong");
        }
    }
}
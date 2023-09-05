using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Wrappers;

namespace Blog.WebAPI.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        var result = string.Empty;
        Response response;

        switch (exception)
        {
            case Blog.Application.Common.Exceptions.ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                response = new Response(failureType: nameof(Blog.Application.Common.Exceptions.ValidationException),validationException.Failures);

                result = JsonSerializer.Serialize(response);
                break;
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                response = new Response(nameof(NotFoundException), new Dictionary<string, string[]>(){
                        { "", new string[]{ notFoundException.Message } }
                    });

                result = JsonSerializer.Serialize(response);
                break;
            case BadRequestException badRequestException:
                code = HttpStatusCode.BadRequest;
                response = new Response(nameof(BadRequestException), new Dictionary<string, string[]>(){
                        { "", new string[]{ badRequestException.Message } }
                    });

                result = JsonSerializer.Serialize(response);
                break;
            default:
                code = HttpStatusCode.BadRequest;
                response = new Response(nameof(Exception), new Dictionary<string, string[]>(){
                        { "", new string[]{ exception.Message } }
                    });

                result = JsonSerializer.Serialize(response);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}

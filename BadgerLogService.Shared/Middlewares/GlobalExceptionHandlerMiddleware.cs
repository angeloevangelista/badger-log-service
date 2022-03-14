using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgerLogService.Shared.Exceptions;
using BadgerLogService.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BadgerLogService.Shared.Middlewares
{
  public class GlobalExceptionHandlerMiddleware : IMiddleware
  {
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
      ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
      _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      try
      {
        await next(context);
      }
      catch (CustomException customException)
      {
        await HandleCustomExceptionAsync(context, customException);
      }
      catch (Exception exception)
      {
        _logger.LogError($"Unexpected error: {exception}");
        await HandleInternalExceptionAsync(context, exception);
      }
    }

    private static Task HandleInternalExceptionAsync(
      HttpContext context,
      Exception exception
    )
    {
      var response = new ApiResponse<object>
      {
        Success = false,
        Errors = new List<string>() { "Internal Server Error." },
      };

      var json = SerializeObject(response);

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = StatusCodes.Status500InternalServerError;

      return context.Response.WriteAsync(json);
    }

    private static Task HandleCustomExceptionAsync(
      HttpContext context,
      CustomException customException
    )
    {
      var response = new ApiResponse<object>
      {
        Success = false,
        Errors = customException.BrokenRules.ToList(),
      };

      var json = SerializeObject(response);

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = GetStatusCodeForException(customException);

      return context.Response.WriteAsync(json);
    }

    private static int GetStatusCodeForException(
      CustomException customException
    )
    {
      int statusCode = StatusCodes.Status400BadRequest;

      if (customException is BusinessException)
        statusCode = StatusCodes.Status400BadRequest;

      if (customException is AuthException)
        statusCode = StatusCodes.Status401Unauthorized;

      return statusCode;
    }

    private static string SerializeObject(object obj) =>
      JsonConvert.SerializeObject(
        obj,
        new JsonSerializerSettings
        {
          ContractResolver = new CamelCasePropertyNamesContractResolver()
        }
      );
  }
}

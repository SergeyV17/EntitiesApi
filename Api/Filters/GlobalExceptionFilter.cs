using Api.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<GlobalExceptionFilter>();
    }
    
    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An internal server error occurred.");
        
        var response = new ErrorDetails
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "Oops! Sorry! Something went wrong." +
                      "Please contact developer so we can try to fix it.",
        };
        
        context.Result = new ObjectResult(response)
        {
            StatusCode = response.StatusCode,
            DeclaredType = typeof(ErrorDetails),
        };
    }
}
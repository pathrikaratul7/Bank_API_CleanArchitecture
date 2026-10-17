using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bank_API.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
                _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occured", exception.Message);

            var problemDetails = new ProblemDetails()
            {

                Instance = httpContext.Request.Path,
                Extensions = new Dictionary<string, object?> { { "TraceID", httpContext.TraceIdentifier } }
            };

            if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Validation error";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Detail = validationException.Message;
                problemDetails.Extensions.Add("errors", validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()));

            }
            else if (exception is DbUpdateException sql &&
          sql.InnerException is SqlException sqlException &&
          sqlException.Number is 2601 or 2627)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                problemDetails.Title = "Duplicate record exception";
                problemDetails.Detail = sqlException.Message;
                
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Exception occured";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = exception.Message;


            }

            httpContext.Response.ContentType = "application/Problem+json";
           await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}

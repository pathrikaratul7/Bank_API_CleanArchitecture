
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bank_API.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Log the exception with its stack trace.
            _logger.LogError(
                exception,
                "An exception occurred while processing {Method} {Path}",
                httpContext.Request.Method,
                httpContext.Request.Path);

            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path,
                Extensions =
                {
                    ["traceId"] = httpContext.TraceIdentifier
                }
            };

            switch (exception)
            {
                // 400 - Validation errors
                case ValidationException validationException:

                    problemDetails.Status =
                        StatusCodes.Status400BadRequest;

                    problemDetails.Title = "Validation Failed";

                    problemDetails.Detail =
                        "One or more validation errors occurred.";

                    problemDetails.Extensions["errors"] =
                        validationException.Errors
                            .GroupBy(error => error.PropertyName)
                            .ToDictionary(
                                group => group.Key,
                                group => group
                                    .Select(error => error.ErrorMessage)
                                    .ToArray());

                    break;

                // 409 - Duplicate unique key
                case DbUpdateException dbUpdateException
                    when dbUpdateException.InnerException
                        is SqlException sqlException
                        && sqlException.Number is 2601 or 2627:

                    problemDetails.Status =
                        StatusCodes.Status409Conflict;

                    problemDetails.Title = "Duplicate Record";

                    problemDetails.Detail =
                        "A record with the same unique value already exists.";

                    break;

                // 404 - Resource not found
                case KeyNotFoundException:

                    problemDetails.Status =
                        StatusCodes.Status404NotFound;

                    problemDetails.Title = "Resource Not Found";

                    problemDetails.Detail =
                        "The requested resource was not found.";

                    break;

                // 403 - Access denied
                case UnauthorizedAccessException:

                    problemDetails.Status =
                        StatusCodes.Status403Forbidden;

                    problemDetails.Title = "Forbidden";

                    problemDetails.Detail =
                        "You do not have permission to perform this action.";

                    break;

                case BadHttpRequestException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Bad request , please pass required body";
                    problemDetails.Detail = exception.Message;

                    break;

                // 500 - Unexpected exception
                default:

                    problemDetails.Status =
                        StatusCodes.Status500InternalServerError;

                    problemDetails.Title = "Internal Server Error";

                    problemDetails.Detail =
                        "An unexpected error occurred. Please try again.";

                    break;
            }

            httpContext.Response.StatusCode =
                problemDetails.Status!.Value;

            httpContext.Response.ContentType =
                "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(
                problemDetails,
                cancellationToken);

            return true;
        }
    }
}

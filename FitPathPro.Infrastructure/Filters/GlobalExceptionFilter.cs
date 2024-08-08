using EduPrime.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EduPrime.Infrastructure.Filters
{
    /// <summary>
    /// Global exception filter settings
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            OnInternalServerException(context);
        }

        /// <summary>
        /// Configures the internal server exception
        /// </summary>
        /// <param name="context"></param>
        private void OnInternalServerException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(InternalServerException))
            {
                var exception = (InternalServerException)context.Exception;

                var error = new ApiFailure
                {
                    Message = exception.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Success = false
                };

                context.Result = new ObjectResult(error);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }
        }
    }

     /// <summary>
    /// Represents the response when an API fails
    /// TODO: Move this to correct file.
    /// </summary>
    public class ApiFailure
    {
        public string Message { get; set; } = string.Empty;

        public int Status { get; set; }

        public bool Success { get; set; }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Billing.Management.Infra.CrossCutting.Extensions.ExceptionFilter
{
    public sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter>? _logger;

        public ExceptionFilter(ILogger<ExceptionFilter>? logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger?.LogError(context.Exception, context.Exception.Message);

            context.Result = new ObjectResult(context)
            {
                Value = context.Exception.Message,
                StatusCode = context.HttpContext.Response.StatusCode
            };
        }
    }
}

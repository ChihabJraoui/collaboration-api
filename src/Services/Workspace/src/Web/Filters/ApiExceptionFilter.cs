using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Filters
{
    public class ApiExceptionFilter:ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            this._logger = logger;
            this._exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), this.HandleValidationException },
                { typeof(NotFoundException), this.HandleNotFoundException },
                { typeof(BusinessRuleException), HandleBusinessRuleException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            this.HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (this._exceptionHandlers.ContainsKey(type))
            {
                this._exceptionHandlers[type].Invoke(context);
                return;
            }

            this.HandleUnknownException(context);
        }

        #region Handlers
        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Detail = context.Exception.Message
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            this._logger.LogError(context.Exception, details.Title);
            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception.Errors);

            context.Result = new BadRequestObjectResult(details);
            this._logger.LogError($"{details.Title} \n {{Errors}}", exception.Errors);
            context.ExceptionHandled = true;
        }

        private void HandleBusinessRuleException(ExceptionContext context)
        {
            var details = new ProblemDetails()
            {
                Title = "Business rule violation.",
                Detail = context.Exception.Message
            };

            context.Result = new BadRequestObjectResult(details);
            this._logger.LogError(details.Detail);
            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var details = new ProblemDetails()
            {
                Title = "The specified resource was not found.",
                Detail = context.Exception.Message
            };

            context.Result = new NotFoundObjectResult(details);
            this._logger.LogError(details.Detail);
            context.ExceptionHandled = true;
        }
        #endregion
    }
}

    
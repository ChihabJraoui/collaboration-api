using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using System.Text;
using Collaboration.ShareDocs.Application.Common.Enumator;
using Microsoft.Extensions.Logging;

namespace Collaboration.ShareDocs.Application.Common.Behaviours
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> _logger;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                var validationException = new Exceptions.ValidationException(failures);
                //  _logger.LogError(ConstructErrorLogMessage(validationException, request));
                throw validationException;
            }

            return next();
        }

        private string ConstructErrorLogMessage(Exceptions.ValidationException exception, TRequest request)
        {
            var message = new StringBuilder();
            message.AppendLine(
                $"[{LoggingTypes.Validation}] name: {typeof(TRequest).Name} id: {request.GetHashCode()}");
            message.AppendLine(exception.Message);

            foreach (var pair in exception.Errors)
            {
                message.AppendLine(pair.Name);

                message.AppendLine($"    - {pair.Message}");

            }

            return message.ToString();
        }
    }
}

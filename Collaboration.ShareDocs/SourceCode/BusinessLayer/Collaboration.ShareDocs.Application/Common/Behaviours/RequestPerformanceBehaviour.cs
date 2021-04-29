using Collaboration.ShareDocs.Application.Common.Enumator;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Common.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();
            var userId = string.IsNullOrEmpty(_currentUserService.UserId) ? "" : $"userId: {_currentUserService.UserId}";
            _timer.Stop();
            var requestHash = request.GetHashCode();

            if (_timer.ElapsedMilliseconds > 500)
            {

                _logger.LogWarning($"[{LoggingTypes.Performance}] name:{typeof(TRequest).Name} id: {requestHash} ({_timer.ElapsedMilliseconds} milliseconds) {userId}");
            }

            return response;
        }
    }
}

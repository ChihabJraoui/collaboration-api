using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Collaboration.ShareDocs.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IConfiguration _configuration;

        public LoggingBehaviour(ILogger<TRequest> logger,ICurrentUserService currentUserService, IConfiguration configuration  )
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _configuration = configuration;
        }
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;

            _logger.LogInformation("DocumentManagement Request : {Name} {@UserId} Data: {@Request}",
                this._configuration["Application:Name"], requestName, userId, request);

        }
    }
}

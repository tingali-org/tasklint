using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskLint.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private const string InformationMessage = "TaskLint Request: {Name} {@Request}";
        private readonly ILogger _logger;

        public LoggingBehaviour(
            ILogger<TRequest> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task Process(TRequest request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation(InformationMessage, requestName, request);
        }
    }
}

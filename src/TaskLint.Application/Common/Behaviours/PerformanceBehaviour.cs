using MediatR;

using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaskLint.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private const string WarningMessage = "TaskLint Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}";
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(
            ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogWarning(WarningMessage, requestName, elapsedMilliseconds, request);
            }

            return response;
        }
    }
}

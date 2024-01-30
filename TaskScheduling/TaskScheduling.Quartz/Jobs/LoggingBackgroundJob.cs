using Microsoft.Extensions.Logging;
using Quartz;

namespace TaskScheduling.Quartz.Jobs
{
    // Only 1 instace of this job is allowed
    [DisallowConcurrentExecution]
    internal class LoggingBackgroundJob : IJob
    {
        private readonly ILogger<LoggingBackgroundJob> _logger;
        public LoggingBackgroundJob(ILogger<LoggingBackgroundJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Quartz Logging: {DateTime.UtcNow}");

            return Task.CompletedTask;
        }
    }
}

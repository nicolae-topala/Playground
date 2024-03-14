using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsApp.Functions.Functions
{
    public class FunctionTimerTrigger
    {
        private readonly ILogger _logger;

        public FunctionTimerTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FunctionTimerTrigger>();
        }

        [Function("Function")]
        public void Run([TimerTrigger("* * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}

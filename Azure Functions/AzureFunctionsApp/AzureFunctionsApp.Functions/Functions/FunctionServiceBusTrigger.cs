using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsApp.Functions.Functions
{
    public class FunctionServiceBusTrigger
    {
        private readonly ILogger<FunctionServiceBusTrigger> _logger;

        public FunctionServiceBusTrigger(ILogger<FunctionServiceBusTrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(FunctionServiceBusTrigger))]
        public async Task Run(
            [ServiceBusTrigger("az-queue", Connection = "AzureWebJobsServiceBus")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}

using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsApp.Functions.Functions
{
    public class FunctionBlobTrigger
    {
        private readonly ILogger<FunctionBlobTrigger> _logger;

        public FunctionBlobTrigger(ILogger<FunctionBlobTrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(FunctionBlobTrigger))]
        public async Task Run([BlobTrigger("first-container/{name}", Connection = "AzureWebJobsBlob")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
        }
    }
}

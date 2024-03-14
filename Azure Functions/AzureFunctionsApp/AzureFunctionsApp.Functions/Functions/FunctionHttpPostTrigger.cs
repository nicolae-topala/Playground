using AzureFunctionsApp.Common.DTOs.Customer;
using AzureFunctionsApp.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionsApp.Functions.Functions
{
    public class FunctionHttpPostTrigger
    {
        private readonly ILogger<FunctionHttpPostTrigger> _logger;
        private readonly ICustomerRepository _customerRepository;

        public FunctionHttpPostTrigger(ILoggerFactory loggerFactory, ICustomerRepository customerRepository)
        {
            _logger = loggerFactory.CreateLogger<FunctionHttpPostTrigger>();
            _customerRepository = customerRepository;
        }

        [Function("FunctionHttpPostTrigger")]
        [ServiceBusOutput("az-queue", Connection = "AzureWebJobsServiceBus")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "add-customer")] HttpRequest req)
        {
            _logger.LogInformation("Add customers triggered!");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var customer = JsonConvert.DeserializeObject<CreateCustomerDto>(requestBody);

            if (customer is null) return new BadRequestResult();

            var result = await _customerRepository.CreateNewCustomerAsync(customer);
            return new OkObjectResult(result);
        }
    }
}
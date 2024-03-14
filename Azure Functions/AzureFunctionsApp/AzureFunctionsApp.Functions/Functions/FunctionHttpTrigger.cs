using AzureFunctionsApp.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsApp.Functions.Functions
{
    public class FunctionHttpTrigger
    {
        private readonly ILogger<FunctionHttpTrigger> _logger;
        private readonly ICustomerRepository _customerRepository;

        public FunctionHttpTrigger(ILoggerFactory loggerFactory, ICustomerRepository customerRepository)
        {
            _logger = loggerFactory.CreateLogger<FunctionHttpTrigger>();
            _customerRepository = customerRepository;
        }

        [Function("FunctionHttpTrigger")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-all-customers")] HttpRequest req)
        {
            _logger.LogInformation("Get all customers triggered!");
            var result = await _customerRepository.GetAllCustomersAsync();
            return new OkObjectResult(result);
        }
    }
}
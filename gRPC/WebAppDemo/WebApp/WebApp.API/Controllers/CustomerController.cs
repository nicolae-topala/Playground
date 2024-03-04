using Microsoft.AspNetCore.Mvc;
using WebApp.BLL.Interfaces;
using WebApp.Common.DTOs.Customer;

namespace WebApp.API.Controllers
{
    [ApiController]
    [Route("api/Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("all-customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAllCustomersAsync();
            return Ok(result);
        }

        [HttpGet("customer/{name}")]
        public async Task<IActionResult> GetCustomerById(string name)
        {
            var result = await _customerService.GetCustomerByNameAsync(name);
            return Ok(result);
        }

        [HttpPost("customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto customerDto)
        {
            var result = await _customerService.CreateNewCustomerAsync(customerDto);
            return Ok(result);
        }
    }
}

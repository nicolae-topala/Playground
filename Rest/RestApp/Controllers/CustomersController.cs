using Microsoft.AspNetCore.Mvc;
using RestApp.BL.Interfaces;
using RestApp.Common.Dtos;
using RestApp.Domain.Models;

namespace RestApp.API.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService) { 
            _customerService = customerService; 
        }

        [HttpGet(Name = "getAllCustomers")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerService.GetAllCustomersRedisAsync();

            if (result == null || result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{customerId:int}",Name = "getCustomerById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var result = await _customerService.GetCustomerByIdAsync(customerId);

            if (result == null )
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

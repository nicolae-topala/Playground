using Microsoft.AspNetCore.Mvc;
using xUnit.BL.Interfaces;
using xUnit.Common.Dtos;
using xUnit.DAL.Models;

namespace xUnit.API.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet(Name = "getAllCustomers")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerService.GetAllCustomersAsync();

            if (result == null || result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{customerId:Guid}", Name = "getCustomerById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            var result = await _customerService.GetCustomerByIdAsync(customerId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto model)
        {
            var result = await _customerService.CreateCustomerAsync(model);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

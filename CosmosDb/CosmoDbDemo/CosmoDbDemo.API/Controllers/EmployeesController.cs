using CosmoDbDemo.API.DTOs;
using CosmoDbDemo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CosmoDbDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CosmosDbContext _context;

        public EmployeesController(CosmosDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Address = employeeDto.Address,
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Address = employeeDto.Address;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeet(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}

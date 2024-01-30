using Microsoft.AspNetCore.Mvc;
using RabbitMQ.BLL.Interfaces;
using RabbitMQ.Common;

namespace RabbitMQ.ProducerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewStudent(StudentDto studentDto)
        {
            var result = await _studentService.AddNewStudent(studentDto);

            return Ok(result);
        }

    }
}

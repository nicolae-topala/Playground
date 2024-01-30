using RabbitMQ.BLL.Interfaces;
using RabbitMQ.Common;
using RabbitMQ.DAL;
using RabbitMQ.DAL.Models;

namespace RabbitMQ.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _context;
        private readonly IMessageProducer _messagePublisher;

        public StudentService(StudentDbContext context, IMessageProducer messagePublisher)
        {
            _context = context;
            _messagePublisher = messagePublisher;
        }

        public async Task<Guid> AddNewStudent(StudentDto studentDto)
        {
            Student student = new()
            {
                Name = studentDto.Name,
                Age = studentDto.Age,
                CourseTitle = studentDto.CourseTitle
            };

            _context.Student.Add(student);

            await _context.SaveChangesAsync();

            _messagePublisher.SendMessage(student);

            return student.Id;
        }
    }
}

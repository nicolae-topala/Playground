using RabbitMQ.Common;

namespace RabbitMQ.BLL.Interfaces
{
    public interface IStudentService
    {
        Task<Guid> AddNewStudent(StudentDto studentDto);
    }
}

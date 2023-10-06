using MongoDemo.Common.DTOs;

namespace MongoDemo.BLL.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsersAsync();
        Task CreateUserAsync(CreateUserDto user);
        Task<UserDto> FindUserByIdAsync(string userId);
        Task UpdateUserAsync(string userId, UpdateUserDto updatedUser);
        Task DeleteUserByIdAsync(string userId);
        Task<UserDto> FindUserByFirstNameAsync(string userFirstName);
    }
}

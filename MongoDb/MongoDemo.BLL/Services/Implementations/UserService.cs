using MongoDB.Driver;
using MongoDemo.BLL.Services.Interfaces;
using MongoDemo.Common.DTOs;
using MongoDemo.DAL.Models.Implementations;
using MongoDemo.DAL.Repositories.Implementations;

namespace MongoDemo.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<UserModel> _genericRepository;

        public UserService(IMongoRepository<UserModel> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public List<UserDto> GetAllUsersAsync()
        {
            var collection = _genericRepository.AsQueryable();
            var result = collection.Select(user => new UserDto
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
            }).ToList();

            return result;
        }

        public async Task CreateUserAsync(CreateUserDto user)
        {
            var userModel = new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
            };

            await _genericRepository.InsertOneAsync(userModel);
        }

        public async Task<UserDto> FindUserByIdAsync(string userId)
        {
            var user = await _genericRepository.FindByIdAsync(userId).FirstOrDefaultAsync();
            var result = new UserDto
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
            };

            return result;
        }

        public async Task UpdateUserAsync(string userId, UpdateUserDto updatedUser)
        {
            var user = await _genericRepository.FindByIdAsync(userId).FirstOrDefaultAsync();

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.DateOfBirth = updatedUser.DateOfBirth;

            await _genericRepository.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        public async Task DeleteUserByIdAsync(string userId)
        {
            await _genericRepository.DeleteByIdAsync(userId);
        }

        public async Task<UserDto> FindUserByFirstNameAsync(string userFirstName)
        {
            FilterDefinition<UserModel> filter = Builders<UserModel>.Filter.Eq(u => u.FirstName, userFirstName);
            var user = await _genericRepository.FindOneAsync(u => u.FirstName == userFirstName).FirstOrDefaultAsync();
            var result = new UserDto
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
            };

            return result;
        }
    }
}

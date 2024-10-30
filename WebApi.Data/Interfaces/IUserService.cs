
using WebApi.DTOs.User;
using WebApi.Models;

namespace WebApi.Data.Interfaces;

public interface IUserService
{
        public Task<UserModel?> Create(CreateUserDto createUserDto);

        Task<IEnumerable<UserModel>> FindAll();
        public Task<UserModel?> FindOne(int userId);
        public Task<UserModel?> Update(int userid, UpdateUserDto updateUserDto);
        public Task<UserModel> Remove(int userId);

}


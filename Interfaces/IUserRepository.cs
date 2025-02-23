using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Dtos;
using babystepV1.Models;

namespace babystepV1.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(RegisterUserDto userDto);
        Task<bool> IsEmailRegisteredAsync(string email);
        Task<User> GetUserByEmailAsync(string email);
    }
}
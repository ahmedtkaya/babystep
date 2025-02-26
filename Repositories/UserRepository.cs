using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Data;
using babystepV1.Dtos;
using babystepV1.Models;
using Microsoft.EntityFrameworkCore;
using babystepV1.Interfaces;
using BCrypt.Net;

namespace babystepV1.Repositories
{
   public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(RegisterUserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password), // Şifre hashleniyor
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Uuid = Guid.NewGuid(), // Rastgele UUID oluşturuluyor
                
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        
        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> GetUserByIdAsync(Guid userId){
            return await _context.Users.FindAsync(userId);
        }

    }
}
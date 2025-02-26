using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Data;
using babystepV1.Models;
using babystepV1.Repositories;
using babystepV1.Services;
using Microsoft.AspNetCore.Authorization;
using babystepV1.Dtos;
using babystepV1.Interfaces;
using babystepV1.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace babystepV1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        public AuthController(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            if (await _userRepository.IsEmailRegisteredAsync(userDto.Email))
            {
                return BadRequest("This email is already registered");
            }
            await _userRepository.CreateUserAsync(userDto);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            // Kullanıcı yoksa veya şifre doğrulanamıyorsa Unauthorized döndür
            if (user == null || !PasswordHelper.VerifyPassword(loginDto.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            // Kullanıcıdan ID ve diğer gerekli bilgileri al
            var userId = user.Id; // Kullanıcının ID'sini al
            var token = _tokenService.GenerateToken(userId, user.Email); // Token oluştur

            // Geri döndürülecek kullanıcı bilgilerini ayarla
            var userResponse = new
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.Name // Kullanıcının tam adını ekleyebilirsin
            };

            // Token ve kullanıcı bilgileri ile yanıt dön
            return Ok(new { Token = token, User = userResponse });
        }
        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return BadRequest("Invalid user ID format");
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);


        }
    }
}
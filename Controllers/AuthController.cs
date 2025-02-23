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
            if (user == null || !PasswordHelper.VerifyPassword(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token, User = user });
        }
    }
}
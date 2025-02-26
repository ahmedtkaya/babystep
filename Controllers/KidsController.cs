using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using babystepV1.Interfaces;
using babystepV1.Models;
using Microsoft.AspNetCore.Authorization;
using babystepV1.Dtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace babystepV1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KidsController : ControllerBase
    {
        private readonly IKidsRepository _kidsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public KidsController(IKidsRepository kidsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _kidsRepository = kidsRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddKidsAsync([FromBody] CreateKidsDto dto)
        {
            // Model doğrulaması
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kullanıcı kimliğini alma
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User not found");
            }

            var userId = Guid.Parse(userIdClaim.Value);

            // Yeni çocuğu ekleme
            var createdKid = await _kidsRepository.AddKidsToAsync(userId, dto);

            // Başarıyla eklenen çocuğun bilgilerini döndür
            return CreatedAtAction(nameof(GetKid), new { id = createdKid.Id }, createdKid);
        }

        // Çocuğun bilgilerini alma işlemi
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetKid(Guid id)
        {
            var kid = await _kidsRepository.GetKidByIdAsync(id);
            if (kid == null)
            {
                return NotFound();
            }
            return Ok(kid);
        }
        [HttpGet("my-kids")]
        [Authorize]
        public async Task<IActionResult> GetKidsByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not logged in.");
            }

            var kids = await _kidsRepository.GetKidsByUserIdAsync(userId);

            if (kids == null || !kids.Any())
            {
                return NotFound("No kids found for this user.");
            }

            return Ok(kids);
        }
    }
}
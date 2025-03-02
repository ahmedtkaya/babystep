using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using babystepV1.Interfaces;
using Microsoft.AspNetCore.Http;
using babystepV1.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using babystepV1.Services;
using babystepV1.Repositories;



namespace babystepV1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICalculateTimeLeft _calculateTimeLeft;
        public ReminderController(IReminderRepository reminderRepository, IHttpContextAccessor httpContextAccessor, ICalculateTimeLeft calculateTimeLeft)
        {
            _reminderRepository = reminderRepository;
            _httpContextAccessor = httpContextAccessor;
            _calculateTimeLeft = calculateTimeLeft;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReminderAsync([FromBody] CreateReminderDto createReminderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User not found");
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var createdReminder = await _reminderRepository.CreateReminderToAsync(userId, createReminderDto);

            return CreatedAtAction(nameof(GetReminder), new { id = createdReminder.Id }, createdReminder);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetReminder(Guid id)
        {
            var reminder = await _reminderRepository.GetReminderByIdAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            var reminderDetails = new ReminderDetailsDto
            {
                Id = reminder.Id,
                Title = reminder.Title,
                Description = reminder.Description,
                ReminderDate = reminder.ReminderDate,
                TimeLeft = _calculateTimeLeft.CalculateTimeLeft(reminder.ReminderDate)
            };
            return Ok(reminderDetails);

        }

        [HttpGet("my-reminders")]
        [Authorize]
        public async Task<IActionResult> GetAllReminders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User not found");
            }
            var userId = Guid.Parse(userIdClaim.Value);
            var reminders = await _reminderRepository.GetRemindersAsync(userId.ToString());
            return Ok(reminders);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateReminder(Guid id, [FromBody] UpdateReminderDto updateReminderDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User not found");
            }
            var updatedReminder = await _reminderRepository.UpdateReminderAsync(id, updateReminderDto);
            return Ok(updatedReminder);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReminder(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User not found");
            }
            var deletedReminder = await _reminderRepository.DeleteReminderAsync(id);
            return Ok(deletedReminder);
        }

    }
}
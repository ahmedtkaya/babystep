using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Models;
using babystepV1.Interfaces;
using babystepV1.Data;
using Microsoft.EntityFrameworkCore;
using babystepV1.Dtos;


namespace babystepV1.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDbContext _context;
        public ReminderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Reminder> CreateReminderAsync(Reminder reminder)
        {
            try
            {
                await _context.Reminders.AddAsync(reminder);
                await _context.SaveChangesAsync();
                return reminder;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (logging not shown in this example)
                throw new Exception("An error occurred while adding the reminder.", ex);
            }
        }

        public async Task<Reminder?> CreateReminderToAsync(Guid userId, CreateReminderDto createReminderDto)
        {
            var reminder = new Reminder
            {
                Title = createReminderDto.Title,
                Description = createReminderDto.Description,
                ReminderDate = createReminderDto.ReminderDate,
                UserId = userId
            };
            try
            {
                await _context.Reminders.AddAsync(reminder);
                await _context.SaveChangesAsync();
                return reminder;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (logging not shown in this example)
                throw new Exception("An error occurred while adding the reminder.", ex);
            }
        }
        public async Task<Reminder?> GetReminderByIdAsync(Guid id)
        {
            return await _context.Reminders.FindAsync(id);
        }
        public async Task<IEnumerable<Reminder>> GetRemindersAsync(string userId)
        {
            return await _context.Reminders.Where(r => r.UserId.ToString() == userId).ToListAsync();
        }

        public async Task<Reminder?> UpdateReminderAsync(Guid id, UpdateReminderDto updateReminderDto)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null)
            {
                throw new Exception("Reminder not found");
            }

            try
            {
                reminder.Title = updateReminderDto.Title;
                reminder.Description = updateReminderDto.Description;
                reminder.ReminderDate = updateReminderDto.ReminderDate;
                await _context.SaveChangesAsync();
                return reminder;
            }
            catch (DbUpdateException ex)
            {

                throw new Exception("An error occurred while updating the reminder.", ex);
            }

        }

        public async Task<Reminder?> DeleteReminderAsync(Guid id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null)
            {
                throw new Exception("Reminder not found");
            }

            try
            {
                _context.Reminders.Remove(reminder);
                await _context.SaveChangesAsync();
                return reminder;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the reminder.", ex);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Models;
using babystepV1.Dtos;

namespace babystepV1.Interfaces
{
    public interface IReminderRepository
    {
        Task<Reminder> CreateReminderAsync(Reminder reminder);
        Task<Reminder> CreateReminderToAsync(Guid userId, CreateReminderDto createReminderDto);
        Task<Reminder?> GetReminderByIdAsync(Guid id);
        Task<IEnumerable<Reminder>> GetRemindersAsync(string userId);
        Task<Reminder?> UpdateReminderAsync(Guid id, UpdateReminderDto updateReminderDto);
        Task<Reminder?> DeleteReminderAsync(Guid id);

    }
}
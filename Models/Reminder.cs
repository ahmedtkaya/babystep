using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Helpers;
using System.ComponentModel.DataAnnotations;


namespace babystepV1.Models
{
    public class Reminder
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid Uuid { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime ReminderDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Helpers;
using System.ComponentModel.DataAnnotations;

namespace babystepV1.Dtos
{
    public class CreateReminderDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime ReminderDate { get; set; }
    }
}
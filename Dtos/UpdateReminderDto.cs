using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace babystepV1.Dtos
{
    public class UpdateReminderDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}
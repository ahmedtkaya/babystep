using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace babystepV1.Dtos
{
    public class ReminderDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReminderDate { get; set; }
        public string TimeLeft { get; set; }
    }
}
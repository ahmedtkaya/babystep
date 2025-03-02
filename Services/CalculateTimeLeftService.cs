using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Interfaces;

namespace babystepV1.Services
{
    public class CalculateTimeLeftService : ICalculateTimeLeft
    {
        public string CalculateTimeLeft(DateTime reminderDate)
        {
            var today = DateTime.Today;
            if (reminderDate < today)
            {
                return "Expired";
            }

            var daysLeft = (reminderDate - today).Days;
            if (daysLeft < 30)
            {
                return $"{daysLeft} days left";
            }
            var day = reminderDate.Day - today.Day;
            var monthsLeft = (reminderDate.Year - today.Year) * 12 + reminderDate.Month - today.Month;

            if (day < 0)
            {
                monthsLeft--;
                day += DateTime.DaysInMonth(today.Year, today.Month);
            }
            if (monthsLeft > 0)
            {
                return $"{monthsLeft} month {day} day left";
            }
            else
            {
                return $"{day} day left";
            }

            // return $"{monthsLeft} month {day} day left";
        }

    }
}
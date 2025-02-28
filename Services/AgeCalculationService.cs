using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Interfaces;

namespace babystepV1.Services
{
    public class AgeCalculationService : IAgeCalculationService
    {
        public string CalculateAgeDescription(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;

            var months = today.Month - birthDate.Month;
            if (months < 0)
            {
                months += 12;
            }
            var days = (today - birthDate.AddMonths(age * 12 + months)).Days;
            if (age > 0)
            {
                return $"{age} yaşında";
            }
            else if (months > 0)
            {
                return $"{months} aylık";
            }
            else
            {
                return $"{days} günlük";
            }
        }
    }
}
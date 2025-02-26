using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace babystepV1.Dtos
{
    public class CreateKidsDto
    {
        [Required]
        public string Name { get; set; }=string.Empty;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }=string.Empty;
    }
}
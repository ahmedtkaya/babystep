using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using babystepV1.Models;

namespace babystepV1.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        // public string AvatarColor { get; set; }
        
    }
}
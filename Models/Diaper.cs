using System.ComponentModel.DataAnnotations;

namespace babystepV1.Models
{
    public class Diaper
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public Guid KidId { get; set; }
        public Kids Kid { get; set; }
        
        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime ChangeTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}

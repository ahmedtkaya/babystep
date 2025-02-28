using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace babystepV1.Dtos
{
    public class KidDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string AgeDescription { get; set; }
    }
}
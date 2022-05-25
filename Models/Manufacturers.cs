using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KIP_1_API.Models
{
    public partial class Manufacturers
    {
        public Manufacturers()
        {
            Cars = new HashSet<Cars>();
        }

        [Key]
        public long ManufId { get; set; }

        [Required]
        public string Name { get; set; }

        public int YearOfEst { get; set; }

        public string CountryOfOrigin { get; set; }

        public virtual ICollection<Cars> Cars { get; set; }
    }
}

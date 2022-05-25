using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KIP_1_API.Models
{
    public class Cars
    {
        [Key]
        public long CarId { get; set; }

        public long ManufId { get; set; }

        [Required]
        public string ModelName { get; set; }

        public int YearOfProd { get; set; }

        [Required]
        public float EngineValue { get; set; }

        public float EnginePower { get; set; }

        public virtual Manufacturers Manufacturers { get; set; }
    }

}

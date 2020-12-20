using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiCloud.Models
{
    public class Concert
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public double Lat { get; set; }

        [Required]
        public double Long { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        [Required]
        public string AddressName { get; set; }
        public string Description { get; set; }
    }
}
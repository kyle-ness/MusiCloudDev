using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiCloud.Models
{
    public class Concert
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        public string StreetLocation { get; set; }

        public string CityLocation { get; set; }

        public DateTime Date { get; set; }
    }
}

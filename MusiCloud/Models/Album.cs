using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiCloud.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Release_Date { get; set; }

        public string Genre { get; set; }

        public string ImageLink { get; set; }

        // FK from artist table
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist{ get; set; }
    }
}

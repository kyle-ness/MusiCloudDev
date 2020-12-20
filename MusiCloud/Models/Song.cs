using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MusiCloud.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CounterPlayed { get; set; }

        public string LinkToPlay { get; set; }

        // FK from album table
        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}

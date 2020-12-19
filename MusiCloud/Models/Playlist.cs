using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiCloud.Models
{
    public class Playlist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // FK from song table
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

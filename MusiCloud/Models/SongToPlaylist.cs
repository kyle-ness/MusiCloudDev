using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiCloud.Models
{
    public class SongToPlaylist
    {

        // Attempting to create a composite key out of the comination of the song - playlist relationship

        // FK from playlist table
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Playlist")]
        public string PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }

        // FK from song table
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Song")]
        public string SongId { get; set; }
        public virtual Song Song { get; set; }
    }
}

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

        public int Id { get; set; }

        // FK from playlist table
        [ForeignKey("Playlist")]
        public int PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }

        // FK from song table
        [ForeignKey("Song")]
        public int SongId { get; set; }
        public virtual Song Song { get; set; }
    }
}

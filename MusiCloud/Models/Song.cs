using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace MusiCloud.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CounterPlayed { get; set; }

        public string LinkToPlay { get; set; }

        // FK from album table
        [ForeignKey("Album")]
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }

        private readonly ISongsRepository songRepository;
        public IEnumerable<Song> Songs { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
            Songs = songRepository.Search(SearchTerm);
        }
    }

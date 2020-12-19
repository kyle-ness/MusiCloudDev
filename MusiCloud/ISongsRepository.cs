using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusiCloud
{
    public interface ISongsRepository
    {
        IEnumerable<Models.Song> Search(string searchTerm);
    }
}

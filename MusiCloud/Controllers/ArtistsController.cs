using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusiCloud.Data;
using MusiCloud.Models;
using Microsoft.AspNetCore.Authorization;


namespace MusiCloud.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly MusiCloudContext _context;

        public ArtistsController(MusiCloudContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> Artist(String id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userId == null)
            {
                return RedirectToAction("UnauthorizedPage", "Home");
            }

            if (id == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            // Get artist profile
            var artist = await _context.Artist
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            // Number of albums
            int albums_count = _context.Album.Where(m => m.ArtistId.ToString() == id).Count();

            // Number of songs 
            var listOfalbums = (from n in _context.Album where n.ArtistId.ToString() == id select n.Id);
            var count_songs = (from m in _context.Song where listOfalbums.Contains(m.AlbumId) select m).Count();
            var sum_listened = (from m in _context.Song where listOfalbums.Contains(m.AlbumId) select m.CounterPlayed).Sum();

            ViewData["CountedAlbums"] = albums_count;
            ViewData["CountedSongs"] = count_songs;
            ViewData["SumOfListens"] = sum_listened;

            if (artist == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            
            return View(artist);
        }

        [Authorize(Roles = "Admin")]
        // GET: Artists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artist.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        [Authorize(Roles = "Admin")]
        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Genre,ImageLink,ArtistLink")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        [Authorize(Roles = "Admin")]
        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,ImageLink,ArtistLink")] Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artist.FindAsync(id);
            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artist.Any(e => e.Id == id);
        }
    }
}

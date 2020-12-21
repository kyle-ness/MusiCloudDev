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
using System.Web;


namespace MusiCloud.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly MusiCloudContext _context;

        public PlaylistsController(MusiCloudContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> List()
        {
            // Get the user from his current claim and verify it against the database 
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId != null)
            {

                var query = from playlist in _context.Playlist
                            where playlist.UserId.ToString().Equals(userId)
                            select new
                            {
                                name = playlist.Name,
                                imgUrl = playlist.ImageId,
                                playlistId = playlist.Id
                            };


                var playlists = await query.ToListAsync();
                return Json(new { Playlists = playlists });
            }
            return new JsonResult(new object());

        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreatePlaylistAjax(String Name)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userId != null)
            {

                Playlist new_playlist = new Playlist();
                new_playlist.Name = Name;

                new_playlist.UserId = int.Parse(userId);
                Random rnd = new Random();
                new_playlist.ImageId = rnd.Next(1, 16);

                _context.Add(new_playlist);
                await _context.SaveChangesAsync();
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Playlist(int? id)
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

            // Get only the playlist that belong to the user, based on the user's Id from the claim
            var playlist = await _context.Playlist
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId.ToString() == userId);
            if (playlist == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            return View(playlist);
        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPlaylistSongsAjax(String playlistId)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userId != null)
            {

                // Verify that the user owns the playlist 
                var playlist = _context.Playlist.FirstOrDefault(p => p.Id.ToString() == playlistId && p.UserId.ToString() == userId);

                // Get all songs id that are part of the playlist
                var listOfSongs = (from n in _context.SongToPlaylist where n.PlaylistId.ToString() == playlistId select n.SongId);

                // Get all songs and their album data that are part of the playlist
                var query = from s in _context.Song
                            where listOfSongs.Contains(s.Id)
                            join a in _context.Album on s.AlbumId equals a.Id
                            select new
                            {
                                songId = s.Id,
                                name = s.Name,
                                songLink = s.LinkToPlay,
                                album = s.Album.Name,
                                imgLink = s.Album.ImageLink,
                                artistId = s.Album.ArtistId
                            };

                var songs = await query.ToListAsync();
                return Json(new { Songs = songs });
            }

            else
            {
                return new JsonResult(new object());
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlaylist(String playlist_id)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userId == null)
            {
                return RedirectToAction("UnauthorizedPage", "Home");
            }

            if (playlist_id == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            var playlist = await _context.Playlist.FirstOrDefaultAsync(m => m.Id.ToString() == playlist_id && m.UserId.ToString() == userId);
            _context.Playlist.Remove(playlist);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserHome", "Home");
        }


        [Authorize(Roles = "Admin")]
        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            var musiCloudContext = _context.Playlist.Include(p => p.User);
            return View(await musiCloudContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlist
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        [Authorize(Roles = "Admin")]
        // GET: Playlists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "DisplayName");
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserId")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "DisplayName", playlist.UserId);
            return View(playlist);
        }

        [Authorize(Roles = "Admin")]
        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlist.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "DisplayName", playlist.UserId);
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId")] Playlist playlist)
        {
            if (id != playlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "DisplayName", playlist.UserId);
            return View(playlist);
        }

        [Authorize(Roles = "Admin")]
        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlist
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playlist = await _context.Playlist.FindAsync(id);
            _context.Playlist.Remove(playlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(int id)
        {
            return _context.Playlist.Any(e => e.Id == id);
        }
    }
}

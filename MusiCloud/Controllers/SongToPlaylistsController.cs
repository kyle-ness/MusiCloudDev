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
    public class SongToPlaylistsController : Controller
    {
        private readonly MusiCloudContext _context;

        public SongToPlaylistsController(MusiCloudContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: SongToPlaylists
        public async Task<IActionResult> Index()
        {
            var musiCloudContext = _context.SongToPlaylist.Include(s => s.Playlist).Include(s => s.Song);
            return View(await musiCloudContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: SongToPlaylists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songToPlaylist = await _context.SongToPlaylist
                .Include(s => s.Playlist)
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songToPlaylist == null)
            {
                return NotFound();
            }

            return View(songToPlaylist);
        }

        [Authorize(Roles = "Admin")]
        // GET: SongToPlaylists/Create
        public IActionResult Create()
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "Name");
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "Name");
            return View();
        }

        // POST: SongToPlaylists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlaylistId,SongId")] SongToPlaylist songToPlaylist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(songToPlaylist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "Name", songToPlaylist.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "Name", songToPlaylist.SongId);
            return View(songToPlaylist);
        }

        // GET: SongToPlaylists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songToPlaylist = await _context.SongToPlaylist.FindAsync(id);
            if (songToPlaylist == null)
            {
                return NotFound();
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "Name", songToPlaylist.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "Name", songToPlaylist.SongId);
            return View(songToPlaylist);
        }

        // POST: SongToPlaylists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlaylistId,SongId")] SongToPlaylist songToPlaylist)
        {
            if (id != songToPlaylist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songToPlaylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongToPlaylistExists(songToPlaylist.Id))
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
            ViewData["PlaylistId"] = new SelectList(_context.Playlist, "Id", "Name", songToPlaylist.PlaylistId);
            ViewData["SongId"] = new SelectList(_context.Song, "Id", "Name", songToPlaylist.SongId);
            return View(songToPlaylist);
        }

        [Authorize(Roles = "Admin")]
        // GET: SongToPlaylists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songToPlaylist = await _context.SongToPlaylist
                .Include(s => s.Playlist)
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songToPlaylist == null)
            {
                return NotFound();
            }

            return View(songToPlaylist);
        }

        // POST: SongToPlaylists/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songToPlaylist = await _context.SongToPlaylist.FindAsync(id);
            _context.SongToPlaylist.Remove(songToPlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongToPlaylistExists(int id)
        {
            return _context.SongToPlaylist.Any(e => e.Id == id);
        }
    }
}

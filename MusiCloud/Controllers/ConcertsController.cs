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
    public class ConcertsController : Controller
    {
        private readonly MusiCloudContext _context;

        public ConcertsController(MusiCloudContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> GetConcerts(string Id)
        {
            // Get the user from his current claim and verify it against the database 
            //var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            //if (userId != null)
            
                var query = from concert in _context.Concert
                            where concert.ArtistId.ToString().Equals(Id)
                            select new
                            {
                                lat = concert.Lat,
                                lng = concert.Long,
                                name = concert.Name,
                                date = concert.Date,
                                country = concert.Country,
                                city = concert.City,
                                address = concert.AddressName
                            };


                var concerts = await query.ToListAsync();
                return Json(new { Concerts = concerts });
            
        }

        public IActionResult Show(int? Id)
        {

            var artist_id = Id.ToString();
            var artist = _context.Artist.FirstOrDefault(m => m.Id.ToString() == artist_id);
            
            if (artist != null)
            {
                return View(artist);
            }

            else
            {
                return RedirectToAction("Error404", "Home");
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Concerts
        public async Task<IActionResult> Index()
        {
            var musiCloudContext = _context.Concert.Include(c => c.Artist);
            return View(await musiCloudContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: Concerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.Concert
                .Include(c => c.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        [Authorize(Roles = "Admin")]
        // GET: Concerts/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id");
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ArtistId,Date,Lat,Long,Country,City,AddressName,Description")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", concert.ArtistId);
            return View(concert);
        }

        [Authorize(Roles = "Admin")]
        // GET: Concerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.Concert.FindAsync(id);
            if (concert == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", concert.ArtistId);
            return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ArtistId,Date,Lat,Long,Country,City,AddressName,Description")] Concert concert)
        {
            if (id != concert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertExists(concert.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "Id", "Id", concert.ArtistId);
            return View(concert);
        }

        [Authorize(Roles = "Admin")]
        // GET: Concerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concert = await _context.Concert
                .Include(c => c.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // POST: Concerts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concert = await _context.Concert.FindAsync(id);
            _context.Concert.Remove(concert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertExists(int id)
        {
            return _context.Concert.Any(e => e.Id == id);
        }
    }
}

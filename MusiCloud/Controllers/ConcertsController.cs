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
using System.Net;
using WeatherForecast.Models;
using Newtonsoft.Json;


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
                                lng = concert.Long
                            };


                var concerts = await query.ToListAsync();
                return Json(new { Concerts = concerts });
            
        }

        public IActionResult Show(string Id)
        {

            ViewData["AristId"] = Id;
            return View();
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
        public String WeatherDetail(string City)
        {

            //Assign API KEY
            string appId = "d544b61dcbdf99ece48ec5f7dec17be3";

            //API path with CITY parameter and other parameters.  
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);

            using (WebClient client = new WebClient())
            {
                string json;
                var jsonstring = "";
                try
                {
                    json = client.DownloadString(url);

                    //Converting to OBJECT from JSON string.  
                    RootObject weatherInfo = JsonConvert.DeserializeObject<RootObject>(json);

                    //Special VIEWMODEL design to send only required fields not all fields which received from   
                    //www.openweathermap.org api  
                    ResultViewModel rslt = new ResultViewModel();


                    rslt.Description = weatherInfo.weather[0].description;
                    rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                    rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                    rslt.WeatherIcon = weatherInfo.weather[0].icon;

                    //Converting OBJECT to JSON String   
                    jsonstring = JsonConvert.SerializeObject(rslt);
                }
                catch (Exception)
                {
                    string json1 = @"{ }";
                    jsonstring = JsonConvert.SerializeObject(json1);
                    return jsonstring;

                }
                return jsonstring;
            }
        }
    }
}

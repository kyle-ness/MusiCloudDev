using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusiCloud.Models;
using Microsoft.AspNetCore.Authorization;
using MusiCloud.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;




namespace MusiCloud.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private MusiCloudContext _context;

        public HomeController(ILogger<HomeController> logger, MusiCloudContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (userId == null)
            {
                return View();
            }

            else
            {
                var userType = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                return userType switch
                {
                    "Admin" => RedirectToAction("AdminHome", "Home"),
                    "User" => RedirectToAction("userHome", "Home"),
                    _ => View(),
                };
            }
            
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error404()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult UnauthorizedPage()
        {
            return View();
        }
       

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "User")]
        public IActionResult UserHome()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminHome()
        {
            return View();
        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> Search(String keyWord)
        {
            
            if (keyWord != null || keyWord != "")
            {

                List<Song> results = new List<Song>();

                var songs = await _context.Song.Include(o => o.Album).ThenInclude(bo => bo.Artist).Where(c => c.Name.ToLower().Contains(keyWord.ToLower())).ToListAsync();
                var albums = await _context.Song.Include(o => o.Album).Where(c => c.Album.Name.ToLower().Contains(keyWord.ToLower())).ToListAsync();
                var artists = await _context.Song.Include(o => o.Album).ThenInclude(bo => bo.Artist).Where(c => c.Album.Artist.Name.ToLower().Contains(keyWord.ToLower())).ToListAsync();

                foreach (var song in songs)
                {
                    bool containsItem = results.Any(item => item.Id == song.Id);

                    if (!containsItem)
                    {
                        results.Add(song);
                    }

                }

                foreach (var song in albums)
                {
                    bool containsItem = results.Any(item => item.Id == song.Id);

                    if (!containsItem)
                    {
                        results.Add(song);
                    }

                }

                foreach (var song in artists)
                {
                    bool containsItem = results.Any(item => item.Id == song.Id);

                    if (!containsItem)
                    {
                        results.Add(song);
                    }

                }
                return View(results);
            }

            
            return View();
        }

    }
}

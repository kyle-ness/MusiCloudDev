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
        private readonly MusiCloudContext _context;
        public HomeController(MusiCloudContext context)
        {
            _context = context;
        }

            

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AdminHome()
        {
            var genres = from o in _context.Artist
                         group o by o.Genre into oc
                         select new { Genre = oc.Key, Count = oc.Count() };
            ViewData["genres"] = JsonConvert.SerializeObject(genres.ToArray());

            var places = from o in _context.Concert
                         group o by o.City into oc
                         select new { City = oc.Key, Count = oc.Count() };
            ViewData["places"] = JsonConvert.SerializeObject(places.ToArray());


            return View();
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

        [Authorize]
        public IActionResult UserHome()
        {
            return View();
        }

       
 
    }
}

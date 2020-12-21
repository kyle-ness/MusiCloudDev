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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
    }
}

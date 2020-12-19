using MusiCloud.Data;
using MusiCloud.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusiCloud.Controllers
{
    public class UsersController : Controller
    {

        private MusiCloudContext _context;

        public UsersController(MusiCloudContext context)
        {
            _context = context;
        }


        // GET to Login page
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST to attempt and sign in 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Login(User UserToLogin)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == UserToLogin.Email && u.Password == UserToLogin.Password);

            if (user != null)
            {

                HttpContext.Session.SetString("DisplayName", user.DisplayName.ToString());
                CreateCookie(user);
                return RedirectToAction("Index", "Users");
            }
            else
            {
                ViewBag.error = "Wrong username or password";
            }

            return View();
        }

        // GET the signup page
        public IActionResult SignUp()
        {
            return View();
        }

        // SignUP
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([Bind("Id,DisplayName,Email,Password,ConfirmPassword")] User UserToCreate)
        {

            // Check that we got all the parameters that we need
            if (ModelState.IsValid)
            {

                // Check that the email does not exist
                var check = _context.User.FirstOrDefault(u => u.Email == UserToCreate.Email);
                if (check == null)
                {

                    var NewUser = new User();
                    NewUser.Email = UserToCreate.Email;
                    NewUser.Password = UserToCreate.Password;
                    NewUser.DisplayName = UserToCreate.DisplayName;

                    _context.User.Add(NewUser);
                    await _context.SaveChangesAsync();
                    
                    CreateCookie(NewUser);
                    return RedirectToAction("Index", "Users");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User already exists!");
                }

            }

            return View();
        }

        [AllowAnonymous]
        private async void CreateCookie(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
            };

            await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult UserSettings()
        {


            //if (ModelState.IsValid)
            //{
            //    var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            //    _context.User.Update()


            //}

            return View();
        }
    }
}

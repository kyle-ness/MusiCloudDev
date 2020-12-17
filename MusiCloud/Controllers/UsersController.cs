using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusiCloud.Data;
using MusiCloud.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Login(string Email, string Password)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user != null)
            {

                TempData["DisplayName"] = user.DisplayName;
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
                    TempData["DisplayName"] = NewUser.DisplayName;
                    return RedirectToAction("Index", "Users");

                }
                else

                {
                    ModelState.AddModelError(string.Empty, "User already exists!");
                }

            }

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

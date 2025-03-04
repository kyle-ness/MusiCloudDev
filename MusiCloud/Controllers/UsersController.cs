﻿using MusiCloud.Data;
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

                if (user.UserType == "Admin")
                {
                    return RedirectToAction("AdminHome", "Home");
                }

                else
                {
                    return RedirectToAction("UserHome", "Home");

                }
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
        public async Task<IActionResult> SignUp([Bind("Id,DisplayName,Email,Password,ConfirmPassword,UserType")] User UserToCreate)
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
                    NewUser.UserType = "User";

                    _context.User.Add(NewUser);
                    await _context.SaveChangesAsync();
                    
                    CreateCookie(NewUser);
                    return RedirectToAction("UserHome", "Home");

                }
                else
                {
                    ViewBag.error = "User already exists!";
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
                new Claim(ClaimTypes.Role, user.UserType),
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
        public IActionResult UserSettings()
        {

            var userType = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userType == "Admin")
            {
                ViewData["Type"] = "Admin";
            }

            else
            {
                ViewData["Type"] = "User";
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserSettings(ResetPasswordForUser PasswordReset)
        {


            if (!ModelState.IsValid)
            {
                ViewBag.error = "Invalid parameters";
                return View();
            }

            // Get the variables    
            var CurrentPassword = PasswordReset.CurrentPassword;
            var NewPassword = PasswordReset.NewPassword;
            var ConfirmNewPassword = PasswordReset.ConfirmNewPassword;

            {
                
                // Get the user from his current claim and verify it against the database 
                var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

                if (userId != null)
                {

                    // Check if the password matches in the database
                    var user = _context.User.FirstOrDefault(u => u.Id.ToString() == userId && u.Password == CurrentPassword);

                    if (user != null)
                    {
                        user.Password = NewPassword;
                        await _context.SaveChangesAsync();
                        ViewBag.success = "Password was successfully changed!";
                           
                    }

                    else
                    {
                        ViewBag.error = "Incorrect Password";
                    }
                }
                else
                {
                    ViewBag.error = "No Session";
                }
            }

            return View();
        }
    }
}

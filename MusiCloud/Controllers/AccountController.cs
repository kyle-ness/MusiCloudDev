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
using Microsoft.AspNetCore.Authentication;

namespace MusiCloud.Controllers
{
    public class AccountController : Controller
    {

        private MyTestSite3Context _context;
        
        public AccountController(MyTestSite3Context context)
        {
            _context = context;
        }


        // GET to Login page
        public IActionResult Login()
        {
            return View();
        }
        
        // POST to attempt and sign in 
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            var user = _context.Account.FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user != null)
            {
        
                return RedirectToAction("Index", "Home");
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
        public IActionResult SignUp(string DisplayName, string Email, string Password)
        {
            
            // Check that we got all the parameters that we need
            if(ModelState.IsValid)
            {

                // Check that the email does not exist
                var check = _context.Account.FirstOrDefault(u => u.Email == Email);
                if (check == null)
                {

                    var NewUser = new Account();
                    NewUser.Email = Email;
                    NewUser.Password = Password;
                    NewUser.DisplayName = DisplayName;

                    _context.Account.Add(NewUser);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");

                }
                else
 
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }

            }
            
            return View();
        }
    }
}

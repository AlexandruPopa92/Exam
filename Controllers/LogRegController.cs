using Microsoft.EntityFrameworkCore;
using Exam.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
// Other usings

public class LogRegController : Controller
{
    private ExamContext dbContext;
 
    public LogRegController(ExamContext context)
    {
        dbContext = context;
    }

//------------------------------------------LOGIN PAGE --------------------------------------
    [HttpGet]
    [Route("/")]
    public IActionResult Log()
    {
        return View("index");
    }

//---------------------------------LOG OUT -----------------------------------------------------
    [HttpGet]
    [Route("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Log");
    }


//-----------------------------------------LOGIN LOGIC ------------------------------------------
    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginUser userSubmission)
    {
        if(ModelState.IsValid)
        {
            var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);
            if(userInDb == null)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
                return View("index");
            }

            var hasher = new PasswordHasher<LoginUser>();            
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);            
            if(result == 0)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Email/Password");                    
                return View("index");
            }
            
            var userInfo = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);
            HttpContext.Session.SetInt32("UserId", userInfo.UserId);
            HttpContext.Session.SetString("UserName", userInfo.FirstName);        
            return Redirect("home");
        }

        else
        {
            return View("index");
        }
    }

//-----------------------------------REGISTRATION LOGIC -------------------------------------------
    [HttpPost]
    [Route("register")]
    public IActionResult Register(User user)
    {
        if(ModelState.IsValid)
        {
            if(dbContext.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already in use!");
                return View("index");
            }
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            dbContext.Add(user);
            dbContext.SaveChanges();

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserName", user.FirstName);
            return Redirect("home");
        }
        else
        {
            return View("index");
        }
    }
}
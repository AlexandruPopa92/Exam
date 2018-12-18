using Microsoft.EntityFrameworkCore;
using Exam.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
// Other usings

public class UserController : Controller
{
    private ExamContext dbContext;
 
    public UserController(ExamContext context)
    {
        dbContext = context;
    }

//------------------------------------------SHOW USER PAGE --------------------------------------
    [HttpGet]
    [Route("users/{id}")]
    public IActionResult Show(int id)
    {
        if(HttpContext.Session.GetInt32("UserId") == null)
        {
            return Redirect("/");
        }
        var OneUser = dbContext.Users
            .Include(p=>p.Posted)
            .Include(l=>l.Liked)
            .FirstOrDefault(u=>u.UserId == id);
        return View(OneUser);
    }
}
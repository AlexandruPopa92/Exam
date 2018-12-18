using Microsoft.EntityFrameworkCore;
using Exam.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
// Other usings

public class PostController : Controller
{
    private ExamContext dbContext;
 
    public PostController(ExamContext context)
    {
        dbContext = context;
    }

//------------------------------------- MAIN PAGE --------------------------
    [HttpGet]
    [Route("home")]
    public IActionResult Home()
    { 
        if(HttpContext.Session.GetInt32("UserId") == null)
        {
            return Redirect("/");
        }
        ViewBag.Name = HttpContext.Session.GetString("UserName");
        ViewBag.Id = HttpContext.Session.GetInt32("UserId");

        List<Post> AllPosts = dbContext.Posts
            .Include(c=>c.Creator)
            .Include(a=>a.Likes).OrderByDescending(a=>a.Likes.Count())
            .ToList();
            
        ViewBag.AllPosts = AllPosts;

        return View("index");
        
    }

// ---------------------------------CREATE A POST --------------------------------------
    [HttpPost]
    [Route("create")]
    public IActionResult Create(Post post)
    {
        if(ModelState.IsValid){
            dbContext.Add(post);
            dbContext.SaveChanges();
            return RedirectToAction("Home");
        }
        ViewBag.Id = HttpContext.Session.GetInt32("UserId");
        return RedirectToAction("Home");;
    }
//----------------------------------DELETE POST ------------------------------------------


    [HttpGet]
    [Route("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var OnePost = dbContext.Posts.FirstOrDefault(p=>p.PostId ==id);
        dbContext.Remove(OnePost);
        dbContext.SaveChanges();
        return RedirectToAction("Home");
    }


//----------------------------------CREATE LIKE ----------------------------------
    [HttpPost]
    [Route("createLike")]
    public IActionResult CreateLike(Like like)
    {
        if(dbContext.Likes.Any(u => u.PostId == like.PostId && u.UserId == like.UserId))
            {
                return RedirectToAction("Home");
            }        
        dbContext.Likes.Add(like);
        dbContext.SaveChanges();
        return RedirectToAction("Home");
    }

//-----------------------------SHOW POST ------------------------------------
    [HttpGet]
    [Route("post/{id}")]
    public IActionResult ShowPost(int id)
    {
        if(HttpContext.Session.GetInt32("UserId") == null)
        {
            return Redirect("/");
        }
        var OnePost = dbContext.Posts
            .Include(c=>c.Creator)
            .Include(l=>l.Likes)
                .ThenInclude(u=>u.User)
            .FirstOrDefault(p=>p.PostId ==id);
        return View(OnePost);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult AdminMain()
        {
            return View("AdminMain");
        }

        AppDbContext context;
        public AdminController(AppDbContext appDbContext, IHostingEnvironment appEnv)
        {
            context = appDbContext;
        }


        public IActionResult CreateNewProject()
        {
            
            ViewData["Users"] = new SelectList(context.Users, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewProject(Project project)
        {
            ViewData["Users"] = new SelectList(context.Users, "Id", "FullName");
            if (ModelState.IsValid)
            {
                context.ProjectSet.Add(project);
                context.SaveChanges();
                return View(@"~/Views/Home/Index.cshtml", context);
            }
            else
            {
                return View();
            }
        }
    }
}
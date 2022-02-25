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

        public IActionResult DropDownList()
        {
            return PartialView(context.Users);
        }

        public IActionResult CreateNewProject()
        {
            ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.Status == Status.InWork);
            ViewData["Users"] = context.Users.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewProject(Project project)
        {
            // var selUserStr = (string)selectedUser;
            string selectedUser = Request.Form["selectedUser"].ToString();
            ViewData["Users"] = context.Users.ToList();
            if (ModelState.IsValid)
            {
                var user= context.Users.Where(x => x.FullName == selectedUser).First();
                project.Manager = user;
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
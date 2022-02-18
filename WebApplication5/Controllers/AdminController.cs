using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult AdminMain()
        {
            return View("AdminMain");
        }

      


        public IActionResult CreateNewProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewProjectPost()
        {
            if (ModelState.IsValid)
            {
                return View("CreateNewProject");
            }
            else
            {
                return View("Index");
            }
        }
    }
}
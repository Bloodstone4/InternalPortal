using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminMain()
        {
            return View("AdminMain");
        }
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class CorrectionsController : Controller
    {
        AppDbContext context;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _appEnvironment;
        public CorrectionsController(AppDbContext appDbContext, IHostingEnvironment appEnv)
        {
            context = appDbContext;
            _appEnvironment = appEnv;
           
        }

        public IActionResult ViewCorrections(int? Id)
        {
            if (Id.HasValue)
            {
                var corSet=context.Cors.Where(x => x.Project.Id == Id);
                return View(corSet);
            }
            return View("PageNotFound");
        }
    }
}

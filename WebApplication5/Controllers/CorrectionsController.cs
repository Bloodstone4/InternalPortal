using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        //ViewDataDictionary ViewData;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _appEnvironment;
        public CorrectionsController(AppDbContext appDbContext, IHostingEnvironment appEnv)
        {
            context = appDbContext;
            _appEnvironment = appEnv;
            
        }

        public IActionResult ViewCorrections(int? Id)
        {
            ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.ShowInMenuBar == true);
            if (Id.HasValue)
            {
                var corSet=context.Cors.Where(x => x.Project.Id == Id);
                if (corSet.Count() > 0)
                {                  
                    ViewData["Project"] = context.ProjectSet.Where(x => x.Id == Id).First();
                    return View(corSet);
                }
                
                return View("CorrectionsWereNotFound");
            }
            return View("PageNotFound");
        }

        public IActionResult CompleteCor(int? Id)
        {
            ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.ShowInMenuBar == true);
            var corSet = context.Cors.Where(x => x.Id == Id);
            if (corSet.Count() > 0)
            {                
                return View(corSet.First());
            }
            return View("PageNotFound");
        }

        [HttpPost]
        public IActionResult CompleteCorrection(Corrections cor)
        {
            ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.ShowInMenuBar == true);
            if (ModelState.IsValid)
            {
                context.SaveChanges();
                return View(@"~/Views/Home/Index.cshtml", context);
            }
            else
            {
                return View("CompleteCor");
            }
        }

    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
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
                ViewData["Correction"] = corSet.First();
                return View(); //Передать answer
            }
            return View("PageNotFound");
        }

        [HttpPost]
        public IActionResult CompleteCorrection(Corrections cor)
        {
            ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.ShowInMenuBar == true);
         
                var fileNamePath = cor.Response.ImageFile.FileName;
                    var fileName = Path.GetFileNameWithoutExtension(fileNamePath);
                    var extension = Path.GetExtension(fileNamePath);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    var path = "//images//" + fileName;
                    cor.Response.ImageLink = fileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                    cor.Response.ImageFile.CopyTo(fileStream);
                    }
                    //int idnum;
                    //var res = Int32.TryParse(Request.Form["UserName"], out idnum);
                    //if (res)
                    //{
                    //    var userFound = newCor.Executor = context.Users.Single(x => x.Id == idnum);
                    //    newCor.Executor = userFound;
                    //}


                    context.ResponseSet.Add(cor.Response); //new Corrections(3, new DateTime(2021,6,7), "Everything checked"));
                    context.SaveChanges();
                    return View(@"~/Views/Home/Index", context);           
                
           
        }

        [HttpPost]
        public ViewResult CreateRemark(Corrections newCor)
        {
            if (ModelState.IsValid)
            {
                // "~/images" + fileName;
                //SaveFile
                var fileName = Path.GetFileNameWithoutExtension(newCor.ImageFile.FileName);
                var extension = Path.GetExtension(newCor.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                var path = "//images//" + fileName;
                newCor.ImageLink = fileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    newCor.ImageFile.CopyTo(fileStream);
                }
                int idnum;
                var res = Int32.TryParse(Request.Form["UserName"], out idnum);
                if (res)
                {
                    var userFound = newCor.Executor = context.Users.Single(x => x.Id == idnum);
                    newCor.Executor = userFound;
                }


                context.Cors.Add(newCor); //new Corrections(3, new DateTime(2021,6,7), "Everything checked"));
                context.SaveChanges();
                return View("Index", context);
            }
            else
            {
                return CreateCor();
            }


        }

       
        public ViewResult CreateCor()
        {
            ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.ShowInMenuBar == true);
            ViewBag.NewId = context.Cors.Count() + 1;
            var usersForFillName = context.Users.Where(x => x.FullName == null || x.FullName == string.Empty);
            FillFullName(usersForFillName);
            ViewData["Users"] = new SelectList(context.Users, "Id", "FullName");
            return View();
        }

        public void FillFullName(IQueryable<User> users)
        {
            foreach (var user in users)
            {
                user.FullName = "1"; // нужно вызвать функцию set для автоматического заполнения

            }
            context.SaveChanges();
        }
    }
}

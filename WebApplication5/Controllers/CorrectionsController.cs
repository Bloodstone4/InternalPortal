﻿using Microsoft.AspNetCore.Hosting;
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
                ViewData["Corrections"] = corSet.First();
                return View(); 
            }
            return View("PageNotFound");
        }

        [HttpPost]
        public IActionResult CompleteCorrection(Response response, int? corId)
        {
            ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.ShowInMenuBar == true);
            if (ModelState.IsValid)
            {
                var fileNamePath = response.ImageFile.FileName;
                var fileName = Path.GetFileNameWithoutExtension(fileNamePath);
                var extension = Path.GetExtension(fileNamePath);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                var path = "//images//" + fileName;
                response.ImageLink = fileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    response.ImageFile.CopyTo(fileStream);
                }
                //int idnum;
                //var res = Int32.TryParse(Request.Form["UserName"], out idnum);
                //if (res)
                //{
                //    var userFound = newCor.Executor = context.Users.Single(x => x.Id == idnum);
                //    newCor.Executor = userFound;
                //}


                context.ResponseSet.Add(response); //new Corrections(3, new DateTime(2021,6,7), "Everything checked"));
                context.SaveChanges();
                return View(@"~/Views/Home/Index", context);
            }
            else
            {
                var corSet = context.Cors.Where(x => x.Id == corId);
                ViewData["Corrections"] = corSet.First();
                return View("CompleteCor");
            }
           
        }

        [HttpPost]
        public ActionResult CreateRemark(Corrections newCor, int? ProjectId)
        {
            
            var selectedUser = Request.Form["SelectedUser"].First();
            var userFound = newCor.Executor = context.Users.Single(x => x.FullName ==selectedUser );
            newCor.Executor = userFound;
            if (newCor.Executor != null) ModelState["Executor"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            newCor.Project = context.ProjectSet.Where(x => x.Id == ProjectId).First();

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
                
                context.Cors.Add(newCor); //new Corrections(3, new DateTime(2021,6,7), "Everything checked"));
                context.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.NewId = context.Cors.Count() + 1;
                ViewData["Users"] = context.Users.ToList();
                ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.ShowInMenuBar == true);
                ViewData["Project"] = RespositoryService<Project>.Find(context, ProjectId);
                return View("CreateCor", new Corrections());
            }


        }

       
        public ViewResult CreateCor(int? Id)
        {
            ViewData["Project"]= context.ProjectSet.Where(x => x.Id == Id).First();        
            ViewData["Users"] = context.Users.ToList();
            ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.ShowInMenuBar == true);
            ViewBag.NewId = context.Cors.Count() + 1;
            //var usersForFillName = context.Users.Where(x => x.FullName == null || x.FullName == string.Empty);
            //FillFullName(usersForFillName);
            //ViewData["Users"] = new SelectList(context.Users, "Id", "FullName");
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

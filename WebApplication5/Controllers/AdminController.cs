using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication5.Models;
using System.DirectoryServices;

namespace WebApplication5.Controllers
{
    public class AdminController : Controller
    {
        //public ImportUsersFromAdExecute()
        [HttpPost]
        public IActionResult ClickButton()
        {
            //ImportUsersFromAdExecute();
            ViewData["Logs"] = "След. пользователи были импортированы:";
            return View("AdminMain12");
        }

        
        public IActionResult ImportUsersFromAD()
        {
            List<User> usersNewList = new List<User>();
            List<User> usersUpdateList = new List<User>();
            var usersFromAD = GetUsersFromAD();
            CompareUsers(usersFromAD, ref usersNewList, ref usersUpdateList); // Можно обработать обновление пользователей, также вывести пользователю для подтверждения
            //context.SaveChanges();
            //ImportUsersFromAdExecute();        

            return View(usersNewList);
        }

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
            ViewData["ActiveProjects"] = context.ProjectSet.Where(x => x.ShowInMenuBar == true);
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

        private List<User> GetUsersFromAD()
        {
            string userGrName = "OU=Users";
            var root = new DirectoryEntry("LDAP://" + "oilpro");
            var department = root.Children.Find("OU=Departments");
            List<DirectoryEntry> listGrUsers = new List<DirectoryEntry>();
            foreach (var depart in department.Children)
            {
                var dep = depart as DirectoryEntry;
                var userGrFound = GetGroupFromDepart(dep, userGrName);
                if (userGrFound != null)
                {
                    listGrUsers.Add(userGrFound);
                }
            }
            return GetUserNames(listGrUsers);
        }

            public DirectoryEntry GetGroupFromDepart(DirectoryEntry depart, string groupName)
        {
            var depChildList = depart.ChildrenToList();
            if (depChildList.Count > 0)
            {
                var listFound = depChildList.Where(x => x.Name == groupName);
                if (listFound.Count() == 0)
                {
                    foreach (var dep in depChildList)
                    {
                        var d = dep as DirectoryEntry;
                        GetGroupFromDepart(d, groupName);
                    }
                }
                else
                {
                    return listFound.First();
                }
            }
            return null;
        }

        public List<User> GetUserNames(List<DirectoryEntry> listGrUsers)
        {
            List<User> userList = new List<User>();
            foreach (var userGr in listGrUsers)
            {
                foreach (var user in userGr.Children)
                {
                    var us = user as DirectoryEntry;
                    var fullName = GetProperty("displayName", us);
                    var fullNameSplited = fullName.Split(' ').ToList();
                    if (fullNameSplited.Count == 3)
                    {
                        userList.Add(new User()
                        {
                            LastName = fullNameSplited[0],
                            FirstName = fullNameSplited[1],
                            MiddleName = fullNameSplited[2],
                            AD_GUID = us.NativeGuid,
                            Email = GetProperty("mail", us),
                            Login = GetProperty("mailNickname", us),
                            NameFromAD = "1",
                            FullName = "1"

                        }) ;
                    }
                }
            }
            return userList;
        }

        public string GetProperty(string propName, DirectoryEntry directoryEntry)
        {
            try
            {
                var propert = directoryEntry.Properties[propName];
                return propert.Value as string;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void CompareUsers(List<User> usersFromAD, ref List<User> usersNewList, ref List<User> usersUpdateList)
        {
            foreach (var userFromAD in usersFromAD)
            {
                var userSet = context.Users.Where(x =>x.AD_GUID == userFromAD.AD_GUID);
                if (userSet.Count() > 0)
                {
                    var userFromDB = userSet.First();
                    CompareUpdateAllProps(ref userFromDB, userFromAD);
                }
                else
                {
                    usersNewList.Add(userFromAD);
                }
            }
        }

        public void CompareUpdateAllProps(ref User userFromDB, User userFromAD)
        {
            if (userFromDB.FirstName != userFromAD.FirstName)
            {
                userFromDB.FirstName = userFromAD.LastName;
            }
                 // Остальные свойства      

        }



    }
}
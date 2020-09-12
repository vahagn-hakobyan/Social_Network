using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using WebApplication22_06.Lib;
using WebApplication22_06.Migrations;
using WebApplication22_06.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace WebApplication22_06.Controllers
{
    public class UserController : Controller
    {
        Social baza;
        public UserController(Social baza)
        {
            this.baza = baza;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
       
       
        public IActionResult Add(User obj)
        {
            return Json(obj);
            string error = "";
            if(string.IsNullOrEmpty(obj.name) || string.IsNullOrEmpty(obj.surname)
                || string.IsNullOrEmpty(obj.login)|| string.IsNullOrEmpty(obj.password
                ))
            {
                error = "Please fill all the fields";
               
            }
            else if (obj.password.Length < 6)
            {
                error = "password is too short";
            }
            foreach (char elm in obj.password)
            {
                if (char.IsUpper(elm) == true)
                {
                    error = "";
                }
                else { error = "password should contains Appercase"; }
            }

            string log = obj.login;

          
            var q = (from item in baza.Users where item.login == log select item).Count();
            if (q > 0)
            {
                error = "chexav aper";
            }
            
            if (error == "")
            {
               
                obj.password = SecurePasswordHasher.Hash(obj.password);
                baza.Users.Add(obj);
                baza.SaveChanges();
            }
            else
            {
                TempData["Namak"] = error;
            }
            
            return Redirect("/User/Signup");
           // return Json(obj);

        }
        public IActionResult Mutq(User obj)
        {
            var user = (from item in baza.Users where item.login == obj.login select item).FirstOrDefault();
            if (user == null)
            {
                TempData["Namak"] = "login is wrong";

            }
            else if (user.type == 2)
            {
                TempData["Namak"] = "error";
            }
          
            else if (SecurePasswordHasher.Verify(obj.password, user.password) == false)
            {
                TempData["Namak"] = "password is wrong";
            }

            else if (user.type == 1)
            {
                HttpContext.Session.SetInt32("test", user.Id);
                return Redirect("/User/Admin");
            }
            else
            {
           
                HttpContext.Session.SetInt32("test", user.Id);
                return Redirect("/User/Profile");
            }
            return View();
        }
        public IActionResult Profile()
        {
            int? id = HttpContext.Session.GetInt32("test");
            if (id == null)
            {
                TempData["Namak"] = "please login before...";
                return Redirect("/User/Login");
            }
            var data = (from item in baza.Users where item.Id == id select item).FirstOrDefault();
            ViewBag.currentUser = data;
            
  
            return View();
        }

        public IActionResult Settings()
        {

            return View();
        }
      
        public IActionResult ChangeLogin()
        {
            return View();
        }  
        [HttpPost]
        public IActionResult ChangeLogin(User obj)
        {
            
            int? id = HttpContext.Session.GetInt32("test");
            var data = (from item in baza.Users where item.Id == id select item).FirstOrDefault();
            
            if (SecurePasswordHasher.Verify(obj.password, data.password) == false)
            {
                TempData["Namak"] = "Password sxal e";
                return Redirect("/user/ChangeLogin");

            }
            else
            {
                var log = (from item in baza.Users where item.login == obj.login select item).Count();
                if (log >0)
                {
                    TempData["Namak"] = "Login@ zbaxvac e";
                    return Redirect("/user/ChangeLogin");
                }
                else
                {
                    data.login = obj.login;
                    baza.SaveChanges();
                }
            }

               return Redirect("/user/Profile");
            }

            public IActionResult Logout()
            {
                 HttpContext.Session.Clear();
                return Redirect("/user/login");
            }
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(PasswordViewModel obj)
        {
            int? id = HttpContext.Session.GetInt32("test");
            var pas = (from item in baza.Users where item.Id == id select item).FirstOrDefault();


           if (SecurePasswordHasher.Verify(obj.OldPassword, pas.password) == false)
            {
                TempData["Namak"] = "Password sxal e";
                return Redirect("/user/ChangePassword");

            }
            else if(SecurePasswordHasher.Verify(obj.OldPassword, pas.password) == true)
            {
                pas.password = obj.NewPassword;
                pas.password = SecurePasswordHasher.Hash(pas.password);
                baza.SaveChanges();
            }

            return Redirect("/user/Profile");
        }

        public IActionResult ChangePhoto()
        {
            return View();
        }
        public async Task <IActionResult>  Uploadpic(IFormFile nkar)
        {
            if (nkar.Length > 0)
            {
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string p = unixTimestamp + ".jpg";
                string hasce = "wwwroot/nkarner/" + p;
                using(var strim =new FileStream(hasce, FileMode.Create))
                {
                    await nkar.CopyToAsync(strim);
                }
                int? nk = HttpContext.Session.GetInt32("test");
                var user = (from item in baza.Users where item.Id == nk select item).FirstOrDefault();
                user.photo = p;
                baza.SaveChanges();
            }
            return Redirect("/user/profile");
        } 
        public IActionResult Admin()
        {
            ViewBag.Bazauser = baza.Users;
            return View();
         
        }
      
        public IActionResult Block(int id)
        {

            var data = (from elm in baza.Users where elm.Id == id select elm).FirstOrDefault();
            data.type=2;
            baza.SaveChanges();
          
            return Redirect("/user/admin");

        }
        public IActionResult Unblock(int id)
        {
            var data = (from elm in baza.Users where elm.Id == id select elm).FirstOrDefault();
            data.type = 0;
            baza.SaveChanges();
            return Redirect("/user/admin");
        }
        public IActionResult Search(string id)
        {
            int? me = HttpContext.Session.GetInt32("test");
            var data = (from item in baza.Users 
                        where item.name.StartsWith(id)
                        select new { 
                         id=item.Id,
                         name=item.name,
                         surname=item.surname,
                         areWeFriends=(from elm in baza.Friends
                                       where (elm.User1Id==item.Id && elm.User2Id==me)
                                       || (elm.User2Id == item.Id && elm.User1Id == me) 
                                       select elm ).Count()
                        }).ToList();
            return Json(data);
        }
       


        public IActionResult Friends()
        {
            return View();
        }
        public IActionResult AllRequests()
        {
            return View();
        }
        
    }

    
}
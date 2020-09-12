using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication22_06.Migrations;
using WebApplication22_06.Models;


namespace WebApplication22_06.Controllers
{
    public class FriendsController : Controller
    {
        Social baza;
        public FriendsController(Social baza)
        {
            this.baza = baza;
        }

        public IActionResult AddFriend(int id)
        {
            WebApplication22_06.Models.Requests r = new WebApplication22_06.Models.Requests();
            r.User2Id = id;
            int? me = HttpContext.Session.GetInt32("test");
            r.User1Id = (int)me;
            baza.Requests.Add(r);
            baza.SaveChanges();
            return Json("ok");

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllRequests()
        {
            return View();
        }
        public IActionResult Getmyrequests()
        {
            int? me = HttpContext.Session.GetInt32("test");
            var data = (from item in baza.Requests where item.User2Id == me select new {

                id = item.User1.Id,
                anun = item.User1.name,
                azganun = item.User1.surname
            });

            return Json(data);
        }

        public IActionResult Dashboard()
        {
            int? me = HttpContext.Session.GetInt32("test");
            if (me == null)
            {
                return Redirect("user/login");
            }
            return View();
        }
        public IActionResult Aceptrequest(int id)
        {
            int? me = HttpContext.Session.GetInt32("test");
            var data = (from item in baza.Requests where item.User1Id == id && 
                        item.User2Id==me  select item).FirstOrDefault();
                baza.Requests.Remove(data);
            WebApplication22_06.Models.Friends f = new WebApplication22_06.Models.Friends();
            f.User2Id = (int)me;
            f.User1Id = id;
            baza.Friends.Add(f);

                baza.SaveChanges();
               return Json(data);


        }
        
        public IActionResult Getmyfriends()
        {
            int? me = HttpContext.Session.GetInt32("test");
            var data1 = (from item in baza.Friends
                         where item.User1Id == me
                         select new
                         {
                             id = item.User2.Id,
                             name = item.User2.name,
                             surname = item.User2.surname,
                             nor_namak = 0

                         }
                       ); 
            var data2 = (from item in baza.Friends
                         where item.User2Id == me
                         select new
                         {
                             id = item.User1.Id,
                             name = item.User1.name,
                             surname = item.User1.surname,
                             nor_namak = 0

                         }
                       ) ;



            return Json(data1.Union(data2));
        }
        public IActionResult Delite(int id)
        {
            int? me = HttpContext.Session.GetInt32("test");
            var data = (from item in baza.Requests
                        where item.User1Id == id &&
                        item.User2Id == me
                        select item).FirstOrDefault();
                         baza.Requests.Remove(data);
            return Json(data);

        }

        public IActionResult DeliteFriends(int id)
        {
            int? me = HttpContext.Session.GetInt32("test");
            var data = (from item in baza.Friends
                        where item.User1Id == id &&
                        item.User2Id == me
                        select item).FirstOrDefault();
            baza.Friends.Remove(data);
            return Json(data);

        }
       



    }
}
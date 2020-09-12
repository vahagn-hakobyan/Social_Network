using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication22_06.Models;

namespace WebApplication22_06.Controllers
{
    public class MessangerController : Controller
    {
        Social baza;

        public MessangerController(Social baza)
        {
            this.baza = baza;
        }

        public IActionResult Chat()
        {
            int? me = HttpContext.Session.GetInt32("test");
            if (me == null)
            {
                return Redirect("user/login");
            }
            return View();
        }
        public IActionResult AddMessage([FromBody ]NamakViewModel id)
        {
            int? me = HttpContext.Session.GetInt32("test");

            WebApplication22_06.Models.Namak n = new WebApplication22_06.Models.Namak();
            n.User1Id = (int)me;
            n.User2Id = id.user;
            n.content = id.text;
            baza.Messenger.Add(n);
            baza.SaveChanges();

            return Json(id);
        }
        public IActionResult GetMessages(int id)
        {
            int? me = HttpContext.Session.GetInt32("test");
            var data = (from item in baza.Messenger
                        where (item.User1Id == me && item.User2Id == id)
                        || (item.User2Id == me && item.User1Id == id)
                        select new {
                            fromm = item.User1Id,
                            to = item.User2Id,
                            text = item.content,

                        }).ToList();
            var data2 = (from itm in baza.Messenger
                         where itm.User2Id == me
                            && itm.User1Id == id
                         select itm).ToList();
            foreach (Namak elm in data2)
            {
                if(elm.User2Id==me && elm.User1Id == id)
                {
                    elm.status = 1;
                }
                
            }
            baza.SaveChanges();
            return Json(data);
        }

        public IActionResult Userdetails(int id)
        {
            int? me = HttpContext.Session.GetInt32("test");
            var data = (from item in baza.Messenger
                        where item.User2Id == id
                        select item).FirstOrDefault();
                        

            return Json(data);
        }
        public IActionResult Unreadmessages()
        {
            int? me = HttpContext.Session.GetInt32("test");
            var data = (from elm in baza.Messenger
                        where elm.User2Id == me &&
                        elm.status == 0
                        select elm).ToList().Count;
            return Json(data);
        }
    }
}
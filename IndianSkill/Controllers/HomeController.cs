using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Service;

namespace IndianSkill.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string data = "";
            using (WebClient web1 = new WebClient())
            {
                data = web1.DownloadString(Server.MapPath(@"~\Templates\MailLayout.html"));
            }
        
       // Mailing.SendMail(data.Replace("{Message}", "We have successfully configured SMTP"), "account configured", new List<string>() { "amit90510@gmail.com" , "trackTeamwork@gmail.com" });
            return View();
        }
    }
}
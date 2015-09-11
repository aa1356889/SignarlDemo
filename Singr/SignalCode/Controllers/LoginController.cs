using SignalRDemo.Hubs;
using SignalRDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SignalCode.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessLogin(Users user)
        {
            PushHub hub = null;
            if (this.HttpContext.Application["hub"] == null)
            {
                hub = new PushHub();
                this.HttpContext.Application["hub"] = hub;
            }
            else
            {
                hub = this.HttpContext.Application["hub"] as PushHub;
            }
            FormsAuthentication.SetAuthCookie(user.LoginName, true, FormsAuthentication.FormsCookiePath);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
1, user.LoginName, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.LoginName);
            // generate new identity
            FormsIdentity identity = new FormsIdentity(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            // write to client.
            Response.Cookies.Add(cookie);
            return Json(new { message = "登陆成功", state = 1 });
        }

    }
}

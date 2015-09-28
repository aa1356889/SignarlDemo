using SignalCode.MSMQ;
using SignalRDemo.Hubs;
using SignalRDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;
using System.Web.Mvc;

namespace SignalCode.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Push(string msg, string user)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                PushHub pushHub = new PushHub();
                pushHub.Send(user, msg);
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}

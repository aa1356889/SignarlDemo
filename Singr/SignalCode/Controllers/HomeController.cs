using SignalRDemo.Hubs;
using SignalRDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public JsonResult Push(string msg)
        {
            AjaxResult result = new AjaxResult();
            try
            {
                PushHub pushHub = new PushHub();
                pushHub.Send("aa", "你好啊");
                result.Message = "广播失败!";
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Message = "广播失败!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleChat.Web.Controllers
{
    [RoutePrefix("chat")]
    public class ChatController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
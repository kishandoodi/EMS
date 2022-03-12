using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp_complete.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        [ChildActionOnly]
        public ActionResult Partial()
        {
            return PartialView("_Partial");
        }
    }
}
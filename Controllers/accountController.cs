using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_complete.EMP;
using System.Web.Security;

namespace WebApp_complete.Controllers
{
    public class accountController : Controller
    {
        //GET: account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(account model)    
        {
            using (var context = new EMSEntities1())
            {
                bool isValid = context.accounts.Any(x=>x.username == model.username && x.password == model.password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.username, false);   
                    return RedirectToAction("Welcome", "EMP");
                }
                ModelState.AddModelError("", "invalid user name and password");
            }
            return View();
        }
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]

        public ActionResult signup(account model)
        {
            using (var context = new EMSEntities1())
            {
                context.accounts.Add(model);
                context.SaveChanges();
                // ok?


            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout(account model)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }
}
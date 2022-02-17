using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_complete.EMP;
using System.Web.Security;

namespace WebApp_complete.Controllers
{
    public class AccountController : Controller
    {
        //GET: account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User model)    
        {
            using (var context = new EMSEntities())
            {
                bool isValid = context.Users.Any(x=>x.username == model.username && x.password == model.password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.username, false);   
                    return RedirectToAction("Welcome", "EMP");
                }
                ModelState.AddModelError("", "invalid user name and password");
            }
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Signup(User model)
        {
            using (var context = new EMSEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
                // ok?....


            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }
}
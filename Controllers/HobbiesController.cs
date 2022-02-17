using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_complete.EMP;
using WebApp_complete.Models;

namespace WebApp_complete.Controllers
{
    public class HobbiesController : Controller
    {
        readonly EMSEntities dbObj = new EMSEntities();

        // GET: Hobbies
        public ActionResult Index()
        {
            var hob = dbObj.Hobbies.Select(s=>new HobbiesModel()
            {
               Id=s.HobbiesId,
                Hobbies=s.HobbyName,
                CreatedDateTime=s.CreatedDateTime,
                ModifiedDateTime=s.ModifiedDateTime

            }).ToList();
            return View(hob);

        }
        public ActionResult Create()
        {
            var obj = new HobbiesModel();
            if (obj != null)
                return View(obj);
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(HobbiesModel Model)
        {
            Hobby obj = new Hobby();
            obj.HobbyName = Model.Hobbies;

            obj.CreatedDateTime = DateTime.Now;
            obj.ModifiedDateTime = DateTime.Now;
            dbObj.Hobbies.Add(obj);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var edit = dbObj.Hobbies.Where(x => x.HobbiesId == Id).Select(e => new HobbiesModel()

            {
                Hobbies = e.HobbyName,
                CreatedDateTime = e.CreatedDateTime


            }).FirstOrDefault();




            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(HobbiesModel model) 
        {
            var hobby = dbObj.Hobbies.Where(s => s.HobbiesId == model.Id).FirstOrDefault();
            //hobby.HobbiesId = model.Id;
            hobby.HobbyName = model.Hobbies;
            hobby.ModifiedDateTime  = DateTime.Now;
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var mapping = dbObj.EmployeeHobbiesMapings.Where(x => x.HobbiesId == id).ToList();

            dbObj.EmployeeHobbiesMapings.RemoveRange(mapping);
            var res = dbObj.Hobbies.Where(x => x.HobbiesId == id).First();
            dbObj.Hobbies.Remove(res);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int ID)
        {
            var hob = dbObj.Hobbies.Where(x => x.HobbiesId == ID).Select(e => new HobbiesModel()
            {
                Id=e.HobbiesId,
                Hobbies=e.HobbyName,
                CreatedDateTime=e.CreatedDateTime,
                ModifiedDateTime=e.ModifiedDateTime,

            }).FirstOrDefault();
            return View(hob);
        }
    }
}







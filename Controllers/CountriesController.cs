using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp_complete.Data;
using WebApp_complete.Models;

namespace WebApp_complete.Controllers
{
    public class CountriesController : Controller
    {
        private EMSEntities db = new EMSEntities();

        // GET: Countries
        public ActionResult Index()
        {
            var res = db.Countries.Select(s => new CountryModel()
            {
                Id = s.CountryId,
                Name = s.CountryName,
                Code = s.CountryCode,
                CreatedDateTime = s.CreatedDateTime,
                ModifiedDateTime = s.ModifiedDateTime

            }).ToList();
            return View(res);

        }
        public ActionResult Create()
        {
            var obj = new CountryModel();
            

            if (obj != null)
                return View(obj);
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(CountryModel Model)
        {
            Country obj = new Country();
            obj.CountryName = Model.Name;
            obj.CountryCode = Model.Code;
            obj.CountryId = Model.Id;
            obj.CreatedDateTime = DateTime.Now;
            db.Countries.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var edit = db.Countries.Where(x => x.CountryId == Id).Select(e => new CountryModel()

            {
                Name = e.CountryName,
               
                Code = e.CountryCode,
                Id = e.CountryId
            }).FirstOrDefault();


           

            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(CountryModel model)
        {
            var obj = db.Countries.Where(s => s.CountryId == model.Id).FirstOrDefault();

            obj.CountryName = model.Name;
            obj.ModifiedDateTime = DateTime.Now;
            obj.CountryCode = model.Code;
            obj.CountryId = model.Id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var mapping = db.Countries.Where(x => x.CountryId == id).First();

            db.Countries.Remove(mapping);
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int ID)
        {
            var hob = db.Countries.Where(x => x.CountryId == ID).Select(e => new CountryModel()
            {
                Id = e.CountryId,
                Name = e.CountryName,
                Code=e.CountryCode,
                CreatedDateTime = e.CreatedDateTime,
                ModifiedDateTime = e.ModifiedDateTime,

            }).FirstOrDefault();
            return View(hob);
        }
    }
}


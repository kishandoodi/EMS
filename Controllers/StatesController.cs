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
    public class StatesController : Controller
    {
        private EMSEntities db = new EMSEntities();

        // GET: States
        public ActionResult Index()
        {
            var State = db.States.Select(s => new StateModel()
            {
                StateId = s.StateId,
                StateName = s.StateName,
                StateCode = s.StateCode,
                Country=s.Country,
                CreatedDateTime = s.CreatedDateTime,
                ModifiedDateTime = s.ModifiedDateTime

            }).ToList();
            return View(State);

        }
        public ActionResult Create()
        {
            var obj = new StateModel();
            var countryList = db.Countries.Select(s => new SelectListItem
            {
                Value = s.CountryId.ToString(),
                Text = s.CountryName
            }).ToList();
            obj.Countries = countryList;

            if (obj != null)
                return View(obj);
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(StateModel Model)
        {
            State obj = new State();
            obj.StateName = Model.StateName;
            obj.StateCode = Model.StateCode;
            obj.CountryId = Model.CountryId;
            obj.CreatedDateTime = DateTime.Now;
            db.States.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var edit = db.States.Where(x => x.StateId == Id).Select(e => new StateModel()

            {
                StateId=e.StateId,
                StateName = e.StateName,

                StateCode = e.StateCode,
                CountryId = e.CountryId
            }).FirstOrDefault();


            var countryList = db.Countries.Select(s => new SelectListItem
            {
                Value = s.CountryId.ToString(),
                Text = s.CountryName
            }).ToList();
            edit.Countries = countryList;

            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(StateModel model)
        {
            var obj = db.States.Where(s => s.StateId == model.StateId).FirstOrDefault();
           
            obj.StateName = model.StateName;
            obj.ModifiedDateTime = DateTime.Now;
            obj.StateCode = model.StateCode;
            obj.CountryId = model.CountryId;    
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var mapping = db.Countries.Where(x => x.CountryId == id).ToList();

            db.Countries.RemoveRange(mapping);
            var res = db.States.Where(x => x.StateId == id).First();
            db.States.Remove(res);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int ID)
        {
            var hob = db.States.Where(x => x.StateId == ID).Select(e => new StateModel()
            {
                StateId = e.StateId,
                StateName = e.StateName,
                StateCode = e.StateCode,
               Country=e.Country,
                CreatedDateTime = e.CreatedDateTime,
                ModifiedDateTime = e.ModifiedDateTime,

            }).FirstOrDefault();
            return View(hob);
        }
    }
}

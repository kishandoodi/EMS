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
    public class DepartmentsController : Controller
    {
        private readonly EMSEntities db = new EMSEntities();

        public ActionResult Index()
        {
            var hob = db.Departments.Select(s => new DepartmentModel()
            {
                ID = s.DepartmentID,
                DepartmentName = s.DepartmentName,
                CreateOn = s.CreatedDateTime,
                ModifiedOn = s.ModifiedDateTime

            }).ToList();
            return View(hob);

        }
        public ActionResult Create()
        {
            var obj = new DepartmentModel();
            if (obj != null)
                return View(obj);
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(DepartmentModel Model)
        {
            Department obj = new Department();
            obj.DepartmentName = Model.DepartmentName;

            obj.CreatedDateTime = DateTime.Now;
            obj.ModifiedDateTime = DateTime.Now;
            db.Departments.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var edit = db.Departments.Where(x => x.DepartmentID == Id).Select(e => new DepartmentModel()

            {
                DepartmentName = e.DepartmentName,
                CreateOn = e.CreatedDateTime


            }).FirstOrDefault();




            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(DepartmentModel model)
        {
            var Department = db.Departments.Where(s => s.DepartmentID == model.ID).FirstOrDefault();
            //hobby.HobbiesId = model.Id;
            Department.DepartmentName = model.DepartmentName;
            Department.ModifiedDateTime = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var mapping = db.Employees.Where(x => x.DepartmentID == id).ToList();

            db.Employees.RemoveRange(mapping);
            var res = db.Departments.Where(x => x.DepartmentID == id).First();
            db.Departments.Remove(res);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int ID)
        {
            var Depart = db.Departments.Where(x => x.DepartmentID == ID).Select(e => new DepartmentModel()
            {
                ID = e.DepartmentID,
                DepartmentName = e.DepartmentName,
                CreateOn = e.CreatedDateTime,
                ModifiedOn = e.ModifiedDateTime,

            }).FirstOrDefault();
            return View(Depart);
        }
    }
}
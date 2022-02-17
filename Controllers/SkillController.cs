using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_complete.EMP;
using WebApp_complete.Models;

namespace WebApp_complete.Controllers
{
    public class SkillController : Controller
    {
        readonly EMSEntities dbObj = new EMSEntities();
        // GET: Skill
        public ActionResult Index()
        {
            var skillsList = dbObj.Skills.Select(sk => new SkillModel()
            {
                Id = sk.SkillId,
                SkillName = sk.SkillName,
                CreatedOn = sk.CreatedDateTime,
                ModifiedOn = sk.ModifiedDateTime
            }).ToList();

            return View(skillsList);
        }
        public ActionResult Create()
        {
            var obj = new SkillModel();
            if (obj != null)
                return View(obj);
            else
                return View();
        }




        [HttpPost]
        public ActionResult Create(SkillModel Model)
        {
            Skill obj = new Skill();
            obj.SkillName = Model.SkillName;

            obj.CreatedDateTime = DateTime.Now;
           
            dbObj.Skills.Add(obj);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var edit = dbObj.Skills.Where(x => x.SkillId == Id).Select(e => new SkillModel()

            {
                SkillName = e.SkillName,
                CreatedOn = e.CreatedDateTime


            }).FirstOrDefault();




            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(SkillModel Model)
        {
            var Skill = dbObj.Skills.Where(s => s.SkillId == Model.Id).FirstOrDefault();
            Skill.SkillName = Model.SkillName;
            Skill.ModifiedDateTime = DateTime.Now;
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var mapping = dbObj.EmployeeSkillMappings.Where(x => x.SkillID == id).ToList();

            dbObj.EmployeeSkillMappings.RemoveRange(mapping);
            var res = dbObj.Skills.Where(x => x.SkillId == id).First();
            dbObj.Skills.Remove(res);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int Id)
        {
            var Skills = dbObj.Skills.Where(a =>a.SkillId==Id).Select( e=> new SkillModel()
            {
                SkillName = e.SkillName,
                Id = e.SkillId,
                CreatedOn=e.CreatedDateTime,
                ModifiedOn=e.ModifiedDateTime,
                
            }).FirstOrDefault();
            return View(Skills);
        }
    }

}
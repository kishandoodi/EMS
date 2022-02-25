using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_complete.Data;

using WebApp_complete.Models;

namespace WebApp_complete.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        readonly EMSEntities dbObj = new EMSEntities();



        public ActionResult Create()
        {
            var obj = new EmployeeModel();
            var stateList = dbObj.States.Select(s => new SelectListItem
            {
                Text = s.StateName,
                Value = s.StateId.ToString()
            }).ToList();
            var countryList = dbObj.Countries.Select(s => new SelectListItem
            {
                Value = s.CountryId.ToString(),
                Text = s.CountryName
            }).ToList();
            var DepartmentList = dbObj.Departments.Select(s => new SelectListItem
            {
                Value = s.DepartmentID.ToString(),
                Text = s.DepartmentName
            }).ToList();

            var skills = dbObj.Skills.Select(s => new CheckModel
            {
                Id = s.SkillId,
                Name = s.SkillName
            }).ToList();
            var Hobbies = dbObj.Hobbies.Select(s => new CheckModel
            {
                Id = s.HobbiesId,
                Name = s.HobbyName
            }).ToList();



            obj.Countries = countryList;
            obj.States = stateList;
            obj.Departments = DepartmentList;
            obj.Skills = skills;
            obj.Hobbies = Hobbies;


            if (obj != null)
                return View(obj);
            else
                return View();

        }

        [HttpPost]
        public ActionResult Create(EmployeeModel model, HttpPostedFileBase file)
        {
            Employee obj = new Employee();
            if (ModelState.IsValid)
            {
                obj.Empid = model.Empid;
                obj.Fname = model.Fname;
                obj.Lname = model.Lname;
                obj.Age = model.Age;
                obj.Doj = model.Doj;
                obj.Pan = model.Pan;
                obj.Aadhar = model.Aadhar;
                obj.Salary = model.Salary;
                obj.CreatedDateTime = DateTime.Now;
                obj.DepartmentID = model.DepartmentID;

                obj.Address = new Address();

                obj.Address.AddressLine1 = model.AddressLine1;
                obj.Address.AddressLine2 = model.AddressLine2;
                obj.Address.ZipCode = model.ZipCode;
                obj.Address.LandMark = model.LandMark;
                obj.Address.StateId = model.StateId;
                obj.Address.CountryId = model.CountryId;
                obj.Address.CreatedDateTime = DateTime.Now;

                #region Skill Logic
                if (model.Skills != null && model.Skills.Count > 0)
                {
                    var selectedSkills = model.Skills.Where(a => a.Checked == true).ToList();
                    if (selectedSkills != null && selectedSkills.Count > 0)
                    {
                        var employeeSkillMappings = new List<EmployeeSkillMapping>();
                        foreach (var abc in selectedSkills)
                        {
                            var empSkill = new EmployeeSkillMapping();
                            empSkill.SkillID = abc.Id;
                            empSkill.EmployeeId = obj.Empid;
                            employeeSkillMappings.Add(empSkill);
                        }
                        obj.EmployeeSkillMappings = employeeSkillMappings;
                    }
                }
                #endregion

                #region Hobbies Logic
                if (model.Hobbies != null && model.Hobbies.Count > 0)
                {
                    var selectedHobbies = model.Hobbies.Where(a => a.Checked == true).ToList();
                    if (selectedHobbies != null && selectedHobbies.Count > 0)
                    {
                        var employeeHobbiesMapings = new List<EmployeeHobbiesMaping>();
                        foreach (var abc in selectedHobbies)
                        {
                            var empHobby = new EmployeeHobbiesMaping();
                            empHobby.HobbiesId = abc.Id;
                            empHobby.EmployeeId = obj.Empid;
                            employeeHobbiesMapings.Add(empHobby);
                        }
                        obj.EmployeeHobbiesMapings = employeeHobbiesMapings;
                    }
                }

                #endregion

                dbObj.Employees.Add(obj); // To Insert record
                dbObj.SaveChanges();
                UploadImage(obj.Empid, file);
            }
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            // SELECT MAXIMUM 2 or 3 Skills and Hobbies, to display the List of Employees nice.
            var emp = dbObj.Employees.Select(e => new EmployeeModel
            {
                Empid = e.Empid,
                Fname = e.Fname,
                Lname = e.Lname,
                Doj = e.Doj,
                Age = e.Age,
                Pan = e.Pan,
                Salary = e.Salary,
                Aadhar = e.Aadhar,
                Department = e.Department,
                Address = e.Address,

                Skills = e.EmployeeSkillMappings.Select(a => new CheckModel
                {
                    Name = a.Skill.SkillName,
                    Id = a.Skill.SkillId,
                    Checked = true
                }).ToList(),


                Hobbies = e.EmployeeHobbiesMapings.Select(a => new CheckModel
                {
                    Name = a.Hobby.HobbyName,
                    Id = a.Hobby.HobbiesId,
                    Checked = true
                }).ToList(),


                MediaFiles = e.MediaFiles.Select(a => new ImageModel
                {

                    FileName = a.FileName,
                    FilePath = a.FilePath + "/" + a.FileName
                }).FirstOrDefault()


            }).ToList();


            return View(emp);
        }

        public ActionResult Edit(int Id)
        {
            var emp = dbObj.Employees.Where(x => x.Empid == Id).Select(
            e => new EmployeeModel()
            {
                Empid = e.Empid,
                Fname = e.Fname,
                Lname = e.Lname,
                Doj = e.Doj,
                Age = e.Age,
                Pan = e.Pan,
                Salary = e.Salary,
                Aadhar = e.Aadhar,
                DepartmentID = e.DepartmentID,
                AddressLine1 = e.Address.AddressLine1,
                AddressLine2 = e.Address.AddressLine2,
                ZipCode = e.Address.ZipCode,
                LandMark = e.Address.LandMark,
                CountryId = e.Address.CountryId,
                StateId = e.Address.StateId

            }).FirstOrDefault();

            var Image = dbObj.MediaFiles.Where(x => x.Empid == Id).Select(a => new ImageModel
            {

                FileName = a.FileName,
                FilePath = a.FilePath + "/" + a.FileName
            }).FirstOrDefault();

            var stateList = dbObj.States.Select(s => new SelectListItem
            {
                Text = s.StateName,
                Value = s.StateId.ToString()
            }).ToList();
            var countryList = dbObj.Countries.Select(s => new SelectListItem
            {
                Value = s.CountryId.ToString(),
                Text = s.CountryName
            }).ToList();
            var DepartmentList = dbObj.Departments.Select(s => new SelectListItem
            {
                Value = s.DepartmentID.ToString(),
                Text = s.DepartmentName
            }).ToList();

            #region Skills logic
            var masterSkills = dbObj.Skills.Select(s => new CheckModel
            {
                Id = s.SkillId,
                Name = s.SkillName
            }).ToList();

            var empSkillIds = dbObj.EmployeeSkillMappings.Where(a => a.EmployeeId == Id).Select(a => a.SkillID).ToList();

            masterSkills.ForEach(skill =>
             {
                 if (empSkillIds.Contains(skill.Id))
                 {
                     skill.Checked = true;
                 }
             });

            #endregion

            #region Hobbies logic
            var masterhobbies = dbObj.Hobbies.Select(s => new CheckModel
            {
                Id = s.HobbiesId,
                Name = s.HobbyName
            }).ToList();
            var hobbyIds = dbObj.EmployeeHobbiesMapings.Where(a => a.EmployeeId == Id).Select(a => a.HobbiesId).ToList();
            masterhobbies.ForEach(Hobby =>
            {
                if (hobbyIds.Contains(Hobby.Id))
                {
                    Hobby.Checked = true;
                }
            });
            #endregion

            emp.Countries = countryList;
            emp.States = stateList;
            emp.Departments = DepartmentList;
            emp.Skills = masterSkills;
            emp.Hobbies = masterhobbies;
            emp.MediaFiles = Image;
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel model, HttpPostedFileBase file)
        {

            var empl = dbObj.Employees.Where(s => s.Empid == model.Empid).FirstOrDefault();
            if (file != null)
            {
                var allowedExtensions = new[]
                {
                ".Jpg", ".png", ".jpg", "jpeg"

             };


                var fileName = Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(file.FileName);

                if (allowedExtensions.Contains(ext))
                {
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    //string myfile = fileName;
                    var path = Path.Combine(Server.MapPath("~/Media/Emp"), fileName);

                    if (empl.MediaFiles.Any())
                    {
                        var tmp = empl.MediaFiles.First();
                        string Old = Request.MapPath("~/Media/Emp/" + tmp.FileName);

                        if (System.IO.File.Exists(Old))
                        {
                            System.IO.File.Delete(Old);
                        }
                        empl.MediaFiles.First().FileName = fileName;
                    }
                    else
                    {
                        MediaFile obj1 = new MediaFile();
                        obj1.FilePath = "Media/Emp";
                        obj1.Empid = model.Empid;
                        obj1.CreatedDateTime = DateTime.Now;
                        obj1.FileName = fileName;
                        obj1.StatusId = 1;
                        empl.MediaFiles = new List<MediaFile>();
                        empl.MediaFiles.Add(obj1);
                    }



                    file.SaveAs(path);
                }
            }
            #region Skills Mapping Logic

            var skillsMappings = empl.EmployeeSkillMappings;
            if (skillsMappings != null && skillsMappings.Count > 0)
            {
                dbObj.EmployeeSkillMappings.RemoveRange(skillsMappings);
            }

            if (model.Skills != null && model.Skills.Count > 0)
            {
                var selectedSkills = model.Skills.Where(a => a.Checked == true).ToList();
                if (selectedSkills != null && selectedSkills.Count > 0)
                {
                    var employeeSkillMappings = new List<EmployeeSkillMapping>();
                    foreach (var abc in selectedSkills)
                    {
                        var empSkill = new EmployeeSkillMapping();
                        empSkill.SkillID = abc.Id;
                        empSkill.EmployeeId = model.Empid;
                        employeeSkillMappings.Add(empSkill);
                    }
                    dbObj.EmployeeSkillMappings.AddRange(employeeSkillMappings);
                }
            }

            # endregion 

            var hobby = dbObj.Employees.Where(s => s.Empid == model.Empid).FirstOrDefault();



            #region Hobbies Mapping Logic
            var hobbyMappings = hobby.EmployeeHobbiesMapings;
            if (hobbyMappings != null && hobbyMappings.Count > 0)
            {
                dbObj.EmployeeHobbiesMapings.RemoveRange(hobbyMappings);
            }
            if (model.Hobbies != null && model.Hobbies.Count > 0)
            {
                var selectedHobbies = model.Hobbies.Where(a => a.Checked == true).ToList();
                if (selectedHobbies != null && selectedHobbies.Count > 0)
                {
                    var EmployeeHobbiesMapings = new List<EmployeeHobbiesMaping>();
                    foreach (var item in selectedHobbies)
                    {
                        var emphobby = new EmployeeHobbiesMaping();
                        emphobby.HobbiesId = item.Id;
                        emphobby.EmployeeId = model.Empid;
                        EmployeeHobbiesMapings.Add(emphobby);
                    }
                    dbObj.EmployeeHobbiesMapings.AddRange(EmployeeHobbiesMapings);
                }
            }
            #endregion

            empl.Fname = model.Fname;
            empl.Lname = model.Lname;
            empl.Doj = model.Doj;
            empl.Age = model.Age;
            empl.Pan = model.Pan;
            empl.Salary = model.Salary;
            empl.Aadhar = model.Aadhar;

            empl.DepartmentID = model.DepartmentID;
            empl.ModifiedDateTime = DateTime.Now;

            empl.Address.AddressLine1 = model.AddressLine1;
            empl.Address.AddressLine2 = model.AddressLine2;
            empl.Address.ZipCode = model.ZipCode;
            empl.Address.LandMark = model.LandMark;
            empl.Address.CountryId = model.CountryId;
            empl.Address.StateId = model.StateId;

            dbObj.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var mapping = dbObj.EmployeeSkillMappings.Where(x => x.EmployeeId == id).ToList();

            dbObj.EmployeeSkillMappings.RemoveRange(mapping);
            var mappingHobby = dbObj.EmployeeHobbiesMapings.Where(x => x.EmployeeId == id).ToList();

            dbObj.EmployeeHobbiesMapings.RemoveRange(mappingHobby);

            var Img = dbObj.MediaFiles.Where(x => x.Empid == id).First();

            dbObj.MediaFiles.Remove(Img);

            var res = dbObj.Employees.Where(x => x.Empid == id).First();
            dbObj.Employees.Remove(res);
            dbObj.SaveChanges();
            var list = dbObj.Employees.ToList();
            return RedirectToAction("List");
        }

        public ActionResult Details(int empId)
        {

            var emp = dbObj.Employees.Where(x => x.Empid == empId).Select(
            e => new EmployeeModel()
            {
                Empid = e.Empid,
                Fname = e.Fname,
                Lname = e.Lname,
                Doj = e.Doj,
                Age = e.Age,
                Pan = e.Pan,
                Salary = e.Salary,
                Aadhar = e.Aadhar,
                Department = e.Department,
                Address = e.Address,
                AddressLine1 = e.Address.AddressLine1,
                AddressLine2 = e.Address.AddressLine2,
                ZipCode = e.Address.ZipCode,
                LandMark = e.Address.LandMark,
                CountryId = e.Address.CountryId,
                StateId = e.Address.StateId,
                MediaFiles = e.MediaFiles.Select(a => new ImageModel
                {

                    FileName = a.FileName,
                    FilePath = a.FilePath + "/" + a.FileName
                }).FirstOrDefault(),

                Skills = e.EmployeeSkillMappings.Select(a => new CheckModel
                {
                    Name = a.Skill.SkillName,
                    Id = a.Skill.SkillId,
                    Checked = true
                }).ToList(),
                Hobbies = e.EmployeeHobbiesMapings.Select(a => new CheckModel
                {
                    Name = a.Hobby.HobbyName,
                    Id = a.Hobby.HobbiesId,
                    Checked = true
                }).ToList()


            }).FirstOrDefault();
            return View(emp);
        }

        public ActionResult Welcome()
        {
            ViewBag.wel = "wel-come to our website";
            return View();
        }

        public ActionResult Sorting(string searching)
        {
            searching = searching != null ? searching.ToLower() : string.Empty;
            return View(dbObj.Employees.Where(x => x.Fname.ToLower().Contains(searching) || x.Lname.ToLower().Contains(searching)).ToList());




        }

        #region Private Methods

        private void UploadImage(int empId, HttpPostedFileBase file)
        {

            #region Image Logic
            try
            {
                if (file != null)
                {
                    MediaFile obj1 = new MediaFile();
                    var allowedExtensions = new[] {
         ".Jpg", ".png", ".jpg", "jpeg"
    };

                    obj1.FilePath = "Media/Emp";
                    obj1.Empid = empId;
                    obj1.CreatedDateTime = DateTime.Now;
                    obj1.StatusId = 1;
                    var fileName = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName);

                    if (allowedExtensions.Contains(ext))
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName);
                        string myfile = fileName;
                        var path = Path.Combine(Server.MapPath("~/Media/Emp"), myfile);

                        obj1.FileName = myfile;
                        dbObj.MediaFiles.Add(obj1);
                        dbObj.SaveChanges();
                        file.SaveAs(path);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            #endregion

        }

        #endregion
    }
}
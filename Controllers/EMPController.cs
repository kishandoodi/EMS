using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApp_complete.EMP;

namespace WebApp_complete.Controllers
{
    [Authorize]
    public class EMPController : Controller
    {
        readonly EMSEntities1 dbObj = new EMSEntities1();
        

        public ActionResult Create(Employee obj)
        {
            if (obj != null)
                return View(obj);
            else
                return View();

        }
         [HttpPost]
        public void Index1(Employee model)
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

                obj.Address = new Address();
                obj.Address.AddressId = model.Address.AddressId; 
                obj.Address.AddressLine1 = model.Address.AddressLine1;
                obj.Address.AddressLine2 = model.Address.AddressLine2;
                obj.Address.ZipCode = model.Address.ZipCode;
                obj.Address.LandMark = model.Address.LandMark;
                obj.Address.StateId = model.Address.StateId;
                obj.Address.CountryId = model.Address.CountryId;
                

                /*obj.Country = new Country();
                obj.Country.CountryId = model.Country.CountryId;
                obj.Country.CountryName = model.Country.CountryName;
                obj.Country.CountryCode = model.Country.CountryCode;


                obj.State = new State();
                obj.State.StateId = model.State.StateId;
                obj.State.StateName = model.State.StateName;
                obj.State.StateCode = model.State.StateCode;*/
                
                //obj.Address.StateId = model.Address.StateId;

                if (model.Empid == 0)
                {
                    dbObj.Employees.Add(obj); // To Insert record
                    dbObj.SaveChanges();

               // obj.Address.Country = new Country();
                //obj.Address.CountryId = model.Address.CountryId;

                //obj.Address.State =

                }

                /*else if (model.AddressId == 0)
                    {
                        dbObj.Employees.Add(obj); 
                        dbObj.SaveChanges();

                    }*/
                
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
            }
            //ModelState.Clear();
            RedirectToAction("List");
        }
        public ActionResult List()
        {
            var res = dbObj.Employees.ToList();
            return View(res);

        }
        public ActionResult Edit(int Id)
        {

            var emp = dbObj.Employees.Where(x => x.Empid == Id).FirstOrDefault();

            return View(emp);
        }

        /*///this is not working//
        public ActionResult Update(EMP_TABLE emp)
        {
            var Fname = emp.Fname;
            var Lname = emp.Lname;
            var Age = emp.Age;
            var Doj = emp.Doj;
            var Pan = emp.Pan;
            var Aadhar = emp.Aadhar;
            var Salary = emp.Salary;    
            return RedirectToAction("Edit");

        }*/
        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            var empl = dbObj.Employees.Where(s => s.Empid == model.Empid).FirstOrDefault();
            empl.Fname = model.Fname;
            empl.Lname = model.Lname;
            empl.Doj = model.Doj;
            empl.Age = model.Age;
            empl.Pan = model.Pan;
            empl.Salary = model.Salary;
            empl.Aadhar = model.Aadhar;

            empl.Address = new Address();
            empl.Address.AddressLine1 = model.Address.AddressLine1;
            empl.Address.AddressLine2 = model.Address.AddressLine2;
            empl.Address.ZipCode = model.Address.ZipCode;
            empl.Address.LandMark = model.Address.LandMark;
            empl.Address.CountryId = model.Address.CountryId;
            empl.Address.StateId=model.Address.StateId;
            dbObj.SaveChanges();
            //dbObj.Remove(empl);
            //dbObj.Add(Id);
            //dbObj.Entry<EMP_TABLE>()..Sta
            return RedirectToAction("List");
        }
        public ActionResult Delete(int id)
        {
            var res = dbObj.Employees.Where(x => x.Empid == id).First();
            dbObj.Employees.Remove(res);
            dbObj.SaveChanges();
            var list = dbObj.Employees.ToList();
            return View("List", list);
        }

        public ActionResult Details(int empId)
        {
            var emp = dbObj.Employees.FirstOrDefault(x => x.Empid == empId);
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



            /* if (searchby == "fname") //this is not working //
            {
                var data = dbObj.EMP_TABLE.Where(model => model.Fname.StartsWith(search) || search == null).ToList();
                return View(data);
            }     
            else if (searchby == "Lname")
            {
                var data = dbObj.EMP_TABLE.Where(model => model.Lname == search || search == null).ToList();
                return View(data);
            }
            else
            {
                var data = dbObj.EMP_TABLE.ToList();
                return View(data);
            }*/

        }
        /* public ActionResult CreateState(State obj)
         {
             return View();
         }
         public void StateCreate(State model)
         {
             State obj = new State();


                 obj.StateId = model.StateId;
                 obj.StateName = model.StateName;
                 obj.StateCode = model.StateCode;


                     dbObj.SaveChanges();



             RedirectToAction("StateList");
         }*/

        public ActionResult StateList()
        {
            var state = dbObj.States.ToList();
            return View(state);

        }
        public ActionResult UpdateState(int Id)
        {

            var state = dbObj.States.Where(x => x.StateId == Id).FirstOrDefault();

            return View(state);
        }
        [HttpPost]
        public ActionResult UpdateState(State model)
        {
            var state = dbObj.States.Where(s => s.StateId == model.StateId).FirstOrDefault();
            state.StateName = model.StateName;
            state.StateCode = model.StateCode;

            dbObj.SaveChanges();

            return RedirectToAction("StateList");

        }
        public ActionResult DeleteState(int id)
        {
            var sta = dbObj.States.Where(x => x.StateId == id).First();
            dbObj.States.Remove(sta);
            dbObj.SaveChanges();
            var list = dbObj.States.ToList(); 
            return View("StateList", list);
        }
        public ActionResult CountryList()
        {
            var Country = dbObj.Countries.ToList();
            return View(Country);
        }
        public ActionResult UpdateCountry(int Id)
        {

            var state = dbObj.Countries.Where(x => x.CountryId == Id).FirstOrDefault();

            return View(state);
        }
        [HttpPost]
        public ActionResult UpdateCountry(Country model)
        {
            var state = dbObj.Countries.Where(s => s.CountryId == model.CountryId).FirstOrDefault();
            state.CountryName = model.CountryName;
            state.CountryCode = model.CountryCode;

            dbObj.SaveChanges();

            return RedirectToAction("CountryList");

        }
        public ActionResult DeleteCountry(int id)
        {
            var country = dbObj.Countries.Where(x => x.CountryId == id).First();
            dbObj.Countries.Remove(country);
            dbObj.SaveChanges();
            var list = dbObj.Countries.ToList();
            return View("CountryList", list);
        }
        public ActionResult SortingState(string searching)
        {
            searching = searching != null ? searching.ToLower() : string.Empty;
            return View(dbObj.States.Where(x => x.StateName.ToLower().Contains(searching) || x.StateCode.ToLower().Contains(searching)).ToList());
        }




        public ActionResult SortingCountry(string searching)
        {

            searching = searching != null ? searching.ToLower() : string.Empty;
            return View(dbObj.Countries.Where(x => x.CountryName.ToLower().Contains(searching) || x.CountryCode.ToLower().Contains(searching)).ToList());

        }
        public ActionResult DropDownCountry(int? defaultCountryId = 1)
        {
            Employee model = new Employee();

             var CountryList = dbObj.Countries.ToList();
            var allStatelist = dbObj.States.Where(m => m.CountryId == defaultCountryId).ToList();
            var defaultStateId = allStatelist.Select(m => m.StateId).FirstOrDefault();


            model.Countries = new SelectList(CountryList, "CountryId", "CountryName", defaultCountryId);
            model.States = new SelectList(allStatelist, "StateId", "StateName", defaultStateId);
           

            return View(model);
        }
       /* public ActionResult DropDownState()
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
           

            var StateList = dbObj.States.ToList();

            ViewBag.StateName = new SelectList(StateList, "StateId", "StateName","StateCode");
           
            return View();
        }*/


    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_complete.EMP;

namespace WebApp_complete.Models
{
    public class EmployeeModel
    {
        

        public int Empid { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        public string Age { get; set; }
        public string Doj { get; set; }
        //[Range(10, 20)]
        public string Pan { get; set; }
        //[Range(10,12)]
        public string Aadhar { get; set; }
        public string Salary { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string LandMark { get; set; }
        public int StateId { get; set; }
        public DateTime CreaterDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int CountryId { get; set; }
        public Address Address { get; set; }
        public Department Department { get; set; }
       
        public int DepartmentID { get; set; }
        
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Departments { get; set; }
        public List<CheckModel> Skills { get; set; }
        public List<CheckModel> Hobbies { get; set; }

        
       
    }
   
    
    
    

}
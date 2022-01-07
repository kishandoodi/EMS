using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_complete.Models
{
    public class EmployeeModel
    {
        public int Empid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Age { get; set; }
        public string Doj { get; set; }
        public string Pan { get; set; }
        public string Aadhar { get; set; }
        public string Salary { get; set; }
        public AddressModel Address { get; set; }
        
        //public int AddressId { get; set; }
        //public int StateId { get; set; }
        //public int CountryId { get; set; }
        //public SelectList Countries { get; set; }
        //public SelectList States { get; set; }
    }
    public class AddressModel
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string LandMark { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }

    }
}
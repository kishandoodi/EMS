//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp_complete.EMP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public partial class Employee
    {
        public int Empid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Age { get; set; }
        public string Doj { get; set; }
        public string Pan { get; set; }
        public string Aadhar { get; set; }
        public string Salary { get; set; }
        public int AddressId { get; set; }
    
        public virtual Address Address { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        
        public SelectList Countries { get; set; }
        public SelectList States { get; set; }

    }

    /*public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }*/
}
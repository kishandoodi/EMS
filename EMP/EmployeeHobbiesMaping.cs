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
    
    public partial class EmployeeHobbiesMaping
    {
        public int EmployeeHobbiesMapingId { get; set; }
        public int EmployeeId { get; set; }
        public int HobbiesId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Hobby Hobby { get; set; }
    }
}

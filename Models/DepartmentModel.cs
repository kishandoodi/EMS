using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_complete.Models
{
    public class DepartmentModel
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<System.DateTime> ModifiedOn{ get; set; }
        public System.DateTime CreateOn { get; set; }
    }
}
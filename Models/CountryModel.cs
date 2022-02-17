using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_complete.Models
{
    public class CountryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
    }
}
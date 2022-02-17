using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_complete.EMP;

namespace WebApp_complete.Models
{
    public class StateModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public int CountryId { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public List<SelectListItem> Countries { get; set; }

        public virtual Country Country { get; set; }

    }
}
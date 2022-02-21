using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApp_complete.EMP;

namespace WebApp_complete.Models
{
    public class ImageModel
    {
        public int ImageId { get; set; }
        public string Title { get; set; }
        [DisplayName("Upload File")]

        public string ImagePath { get; set; }

        public HttpPostedFileBase MyProperty { get; set; }

        public Nullable<int> Empid { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
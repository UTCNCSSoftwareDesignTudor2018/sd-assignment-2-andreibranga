using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.MVC.Models
{
    public class EnrollmentViewModel
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public Nullable<double> Grade { get; set; }

        public virtual string Subject { get; set; }
    }
}
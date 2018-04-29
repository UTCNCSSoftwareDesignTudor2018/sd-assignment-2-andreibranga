using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.MVC.Models
{
    public class SubjectViewModel
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int SubjectYear { get; set; }
        public string Observations { get; set; }
    }
}
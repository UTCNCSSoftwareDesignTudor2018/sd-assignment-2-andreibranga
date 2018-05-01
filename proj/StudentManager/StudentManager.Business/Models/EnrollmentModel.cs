using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Models
{
    public class EnrollmentModel
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public Nullable<double> Grade { get; set; }

        public virtual string Subject { get; set; }
    }
}

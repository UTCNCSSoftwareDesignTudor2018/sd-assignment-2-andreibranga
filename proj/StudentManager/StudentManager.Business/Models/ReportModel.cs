using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Restaurant.Business.Models
{
    public class GradeModel
    {
        public string Subject { get; set; }
        public double Grade { get; set; }

    }
    public class ReportModel
    {
        public ObjectId _id { get; set; }
        public int userId { get; set; }

        public string studentId { get; set; }

        public string studentName { get; set; }

        public double averageMark { get; set; }

        public string currentDateTime { get; set; }

        public List<GradeModel> grades { get; set; }

        public DateTime timeStamp { get; set; }
    }
}

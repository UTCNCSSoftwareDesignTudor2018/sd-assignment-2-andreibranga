using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Business.Models;
using Restaurant.Data.Entities;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant.Data.MongoDB;
using MongoDB.Driver.Linq;

namespace Restaurant.Business.Repos.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private StudentDbEntities ctx;
        private MongoDbConnection connection;
        public ReportsRepository(StudentDbEntities ctx)
        {
            this.ctx = ctx;
            this.connection= new MongoDbConnection("StudentManager", "Reports");
        }
        public ReportModel GetReportForUser(int userId)
        {
            var user = ctx.Users.Single(p => p.UserId == userId);

            var gradesDB = ctx.Enrollments.Where(p => p.UserId == userId);

            ReportModel report=new ReportModel();
            report.userId = user.UserId;
            report.studentId = user.StudentIdNumber;
            report.studentName = user.LastName + " " + user.FirstName;
            report.currentDateTime = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();
            report.timeStamp=DateTime.Now;
            List<GradeModel> grades= new List<GradeModel>();

            foreach (var item in gradesDB)
            {
                GradeModel grade=new GradeModel()
                {
                    Subject = ctx.Subjects.Single(p=>p.SubjectId==item.SubjectId).SubjectName,
                    Grade = item.Grade??0
                };
                grades.Add(grade);
            }

            report.averageMark = gradesDB.Sum(p => p.Grade??0) / gradesDB.Count();
            report.grades = grades;

            return report;
        }

        public void SaveReportForStudent(int userId)
        {
            ReportModel report = GetReportForUser(userId);
            connection.Collection.InsertOne(report.ToBsonDocument());
        }


        public ReportModel GetUserLastReport(int userId)
        {
            var coll = connection.Db.GetCollection<ReportModel>("Reports").AsQueryable();



            return coll.Where(p=>p.userId==userId).OrderByDescending(p=>p.currentDateTime).FirstOrDefault();
        }
    }
    
}

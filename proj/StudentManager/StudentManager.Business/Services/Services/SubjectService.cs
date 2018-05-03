using System;
using System.Linq;
using Restaurant.Business.Models;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Data.Entities;

namespace Restaurant.Business.Services.Services
{
    public class SubjectService : ISubjectRepository
    {
        private StudentDbEntities ctx;

        public SubjectService(StudentDbEntities ctx)
        {
            this.ctx = ctx;
        }

        public IQueryable<SubjectModel> GetAllSubjects()
        {
            return ctx.Subjects.Select(z => new SubjectModel()
            {
                SubjectId = z.SubjectId,
                SubjectName = z.SubjectName,
                SubjectYear = z.SubjectYear,
                Observations = z.Observations
            });
        }

        public void AddSubject(string subjectName, int subjectYear, string observations)
        {
            Subject subject=new Subject()
            {
                SubjectName = subjectName,
                SubjectYear = subjectYear,
                Observations = observations
            };
            ctx.Subjects.Add(subject);
            ctx.SaveChanges();
        }

        public void UpdateSubject(int subjectId, string subjectName, int subjectYear, string observations)
        {
            var subject = ctx.Subjects.Single(p => p.SubjectId == subjectId);
            subject.SubjectName = subjectName;
            subject.SubjectYear = subjectYear;
            subject.Observations = observations;
            ctx.SaveChanges();
        }

        public void DeleteSubject(int subjectId)
        {
            if (ctx.Enrollments.Any(p => p.SubjectId == subjectId))
            {
                throw new Exception("Cannot delete subject assigned to students!");
            }

            var subject = ctx.Subjects.Single(p => p.SubjectId == subjectId);
            ctx.Subjects.Remove(subject);
            ctx.SaveChanges();
        }
    }
}

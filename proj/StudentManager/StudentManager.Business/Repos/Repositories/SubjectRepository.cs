﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Business.Models;
using Restaurant.Data.Entities;

namespace Restaurant.Business.Repos.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private StudentDbEntities ctx;

        public SubjectRepository(StudentDbEntities ctx)
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
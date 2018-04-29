using System.Linq;
using Restaurant.Business.Models;

namespace Restaurant.Business.Repos.Repositories
{
    public interface ISubjectRepository
    {
        IQueryable<SubjectModel> GetAllSubjects();
        void AddSubject(string subjectName, int subjectYear, string observations);
        void UpdateSubject(int subjectId, string subjectName, int subjectYear, string observations);
        void DeleteSubject(int subjectId);
    }
}
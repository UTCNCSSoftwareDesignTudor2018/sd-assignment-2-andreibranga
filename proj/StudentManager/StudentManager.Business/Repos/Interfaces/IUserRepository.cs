using System.Linq;
using Restaurant.Business.Models;

namespace Restaurant.Business.Repos.Repositories
{
    public interface IUserRepository
    {
        UserModel GetUserById(int userId);

        void AddUserProfile(string identityId, string username, int studentCurrentYear, string studentIdNumber,
            string lastName, string firstName);

        void UpdateUserProfileById(int userId, string userName, int studentCurrentYear, string studentIdNumber,
            string lastName, string firstName);

        bool GetUserIdentityHasProfile(string id);
        int GetUserIdForIdentity(string id);
        IQueryable<UserModel> GetAllStudents();
        IQueryable<EnrollmentModel> GetAllStudentEnrollments(int id);
        void AddEnrollment(int userId, int subjectId, double grade);
        void EditEnrollment(int userId, int subjectId, double grade);
    }
}
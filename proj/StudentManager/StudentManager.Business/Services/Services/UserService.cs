using System.Linq;
using Restaurant.Business.Models;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Data.Entities;

namespace Restaurant.Business.Services.Services
{
    public class UserService : IUserRepository
    {
        private StudentDbEntities ctx;

        private ServiceFactory factory;
        private ReportsService reportsRepository;



        public UserService(StudentDbEntities ctx)
        {
            this.ctx = ctx;
            factory=new ServiceFactory(ctx);
            reportsRepository = factory.GetReportsRepository();
        }


        public UserModel GetUserById(int userId)
        {
            var user = ctx.Users.Single(p => p.UserId == userId);
            UserModel model=new UserModel()
            {
                UserId = user.UserId,
                Id = user.Id,
                UserName = user.UserName,
                StudentCurrentYear = user.StudentCurrentYear,
                StudentIdNumber = user.StudentIdNumber,
                LastName = user.LastName,
                FirstName = user.FirstName
            };
            return model;
        }

        public void AddUserProfile(string identityId, string username, int studentCurrentYear, string studentIdNumber,
            string lastName, string firstName)
        {
            User user=new User()
            {
                Id=identityId,
                UserName = username,
                StudentCurrentYear = studentCurrentYear,
                StudentIdNumber = studentIdNumber,
                LastName = lastName,
                FirstName = firstName
            };
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }


        public void UpdateUserProfileById(int userId, string userName, int studentCurrentYear, string studentIdNumber,
            string lastName, string firstName)
        {
            var user = ctx.Users.Single(p => p.UserId == userId);
            user.UserName = userName;
            user.StudentCurrentYear = studentCurrentYear;
            user.StudentIdNumber = studentIdNumber;
            user.LastName = lastName;
            user.FirstName = firstName;

            ctx.SaveChanges();
        }

        public bool GetUserIdentityHasProfile(string id)
        {
            return ctx.Users.Any(p => p.Id == id);
        }

        public int GetUserIdForIdentity(string id)
        {
            var user = ctx.Users.SingleOrDefault(p => p.Id == id);

            return user == null ? 0 : user.UserId;
        }

        public IQueryable<UserModel> GetAllStudents()
        {
            return ctx.Users.Select(z => new UserModel()
            {
                UserId = z.UserId,
                Id = z.Id,
                UserName = z.UserName,
                StudentCurrentYear = z.StudentCurrentYear,
                StudentIdNumber = z.StudentIdNumber,
                LastName = z.LastName,
                FirstName = z.FirstName
            });
        }


        public IQueryable<EnrollmentModel> GetAllStudentEnrollments(int id)
        {
            return ctx.Enrollments.Where(p => p.UserId == id).Select(z => new EnrollmentModel()
            {
                UserId = z.UserId,
                SubjectId = z.SubjectId,
                Grade = z.Grade ?? 0,
                Subject = z.Subject.Observations
            });
        }

        public void AddEnrollment(int userId, int subjectId, double grade)
        {
            Enrollment enrollment=new Enrollment()
            {
                UserId = userId,
                SubjectId = subjectId,
                Grade=grade
            };

            ctx.Enrollments.Add(enrollment);
            ctx.SaveChanges();


            reportsRepository.SaveReportForStudent(userId);
        }

        public void EditEnrollment(int userId,int subjectId, double grade)
        {
            var enrollment = ctx.Enrollments.Single(p => p.UserId == userId && p.SubjectId==subjectId);
            enrollment.Grade = grade;
            ctx.SaveChanges();

            reportsRepository.SaveReportForStudent(userId);
        }


    }
}

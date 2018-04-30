using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Business.Models;
using Restaurant.Data.Entities;

namespace Restaurant.Business.Repos.Repositories
{
    public class UserRepository : IUserRepository
    {
        private StudentDbEntities ctx;





        public UserRepository(StudentDbEntities ctx)
        {
            this.ctx = ctx;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Business.Services.Services;
using Restaurant.Data.Entities;

namespace Restaurant.Business
{
    public class RepositoryFactory
    {
        private StudentDbEntities ctx;
        public RepositoryFactory(StudentDbEntities ctx)
        {
            this.ctx = ctx;
        }

        public UserService GetUserService()
        {
            return new UserService(ctx);
        }

        public SubjectService GetSubjectService()
        {
            return new SubjectService(ctx);
        }

        public ReportsService GetReportsService()
        {
            return new ReportsService(ctx);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Business.Repos.Repositories;
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

        public UserRepository GetUserRepository()
        {
            return new UserRepository(ctx);
        }

        public SubjectRepository GetSubjectRepository()
        {
            return new SubjectRepository(ctx);
        }

        public ReportsRepository GetReportsRepository()
        {
            return new ReportsRepository(ctx);
        }
    }
}

using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class ActiveProjectRepository : RepositoryBase<ActiveProject>, IActiveProjectRepository
    {
        public ActiveProjectRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

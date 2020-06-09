using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class PlatformRepository : RepositoryBase<Platform>, IPlatformRepository
    {
        public PlatformRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

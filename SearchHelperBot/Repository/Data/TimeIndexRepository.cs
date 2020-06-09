using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class TimeIndexRepository : RepositoryBase<TimeIndex>, ITimeIndexRepository
    {
        public TimeIndexRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

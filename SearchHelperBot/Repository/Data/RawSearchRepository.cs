using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class RawSearchRepository : RepositoryBase<RawSearch>, IRawSearchRepository
    {
        public RawSearchRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

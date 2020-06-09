using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class PreferredSearchRepository : RepositoryBase<PreferredSearch>, IPreferredSearchRepository
    {
        public PreferredSearchRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

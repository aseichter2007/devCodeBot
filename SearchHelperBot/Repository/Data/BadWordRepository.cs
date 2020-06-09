using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class BadWordRepository : RepositoryBase<BadWord>, IBadWordRepository
    {
        public BadWordRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

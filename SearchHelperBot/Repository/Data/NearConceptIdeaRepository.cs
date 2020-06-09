using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class NearConceptIdeaRepository : RepositoryBase<NearConceptIdea>, INearConceptIdeaRepository
    {
        public NearConceptIdeaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class NearConceptPhraseRepository : RepositoryBase<NearConceptPhrase>, INearConceptPhraseRepository
    {
        public NearConceptPhraseRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

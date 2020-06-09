using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class BadPhraseRepository : RepositoryBase<BadPhrase>, IBadPhraseRepository
    {
        public BadPhraseRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

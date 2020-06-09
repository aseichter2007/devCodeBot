using Repository.Contracts;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class PreferredLanguageRepository : RepositoryBase<PreferredLanguage>, IPreferredLanguageRepository
    {
        public PreferredLanguageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

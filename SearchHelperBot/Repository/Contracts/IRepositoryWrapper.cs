using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IRepositoryWrapper
    {
        IActiveProjectRepository ActiveProjects { get; }
        IBadPhraseRepository BadPhrases { get; }
        IBadWordRepository BadWords { get; }
        IInstructorRepository Instructors { get; }
        ILanguageRepository Languages { get; }
        INearConceptIdeaRepository NearConceptIdeas { get; }
        INearConceptPhraseRepository NearConceptPhrases { get; }
        IPlatformRepository Platforms { get; }
        IPreferredLanguageRepository PreferredLanguages { get; }
        IRawSearchRepository RawSearches { get; }
        ISettingRepository Settings { get; }
        IPreferredSearchRepository PreferredSearches { get; }
        void Save();
    }
}

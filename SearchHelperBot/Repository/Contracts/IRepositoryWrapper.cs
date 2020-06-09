using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IRepositoryWrapper
    {
        IActiveProjectRepository ActiveProject { get; }
        IBadPhraseRepository BadPhrase { get; }
        IBadWordRepository BadWord { get; }
        IInstructorRepository Instructor { get; }
        ILanguageRepository Language { get; }
        INearConceptIdeaRepository NearConceptIdea { get; }
        INearConceptPhraseRepository NearConceptPhrase { get; }
        IPlatformRepository Platform { get; }
        IPreferredLanguageRepository PreferredLanguage { get; }
        IRawSearchRepository RawSearch { get; }
        ISettingRepository Setting { get; }
        ITimeIndexRepository TimeIndex { get; }
        void Save();
    }
}

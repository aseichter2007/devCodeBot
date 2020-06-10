using Repository.Contracts;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private IActiveProjectRepository _activeProjects;
        private IBadPhraseRepository _badPhrases;
        private IBadWordRepository _badWords;
        private IInstructorRepository _instructors;
        private ILanguageRepository _languages;
        private INearConceptIdeaRepository _nearConceptIdeas;
        private INearConceptPhraseRepository _nearConceptPhrases;
        private IPlatformRepository _platforms;
        private IPreferredLanguageRepository _preferredLanguages;
        private IRawSearchRepository _rawSearches;
        private ISettingRepository _settings;
        private IPreferredSearchRepository _preferredSearches;

        public IActiveProjectRepository ActiveProjects
        {
            get 
            {
                if (_activeProjects == null)
                    _activeProjects = new ActiveProjectRepository(_context);
                return _activeProjects;
            }
        }
        public IBadPhraseRepository BadPhrases
        {
            get 
            {
                if (_badPhrases == null)
                    _badPhrases = new BadPhraseRepository(_context);
                return _badPhrases;
            }
        }
        public IBadWordRepository BadWords
        {
            get
            {
                if (_badWords == null)
                    _badWords = new BadWordRepository(_context);
                return _badWords;
            }
        }
        public IInstructorRepository Instructors
        {
            get
            {
                if (_instructors == null)
                    _instructors = new InstructorRepository(_context);
                return _instructors;
            }
        }
        public ILanguageRepository Languages
        {
            get
            {
                if (_languages == null)
                    _languages = new LanguageRepository(_context);
                return _languages;
            }
        }
        public INearConceptIdeaRepository NearConceptIdeas
        {
            get
            {
                if (_nearConceptIdeas == null)
                    _nearConceptIdeas = new NearConceptIdeaRepository(_context);
                return _nearConceptIdeas;
            }
        }
        public INearConceptPhraseRepository NearConceptPhrases
        {
            get
            {
                if (_nearConceptPhrases == null)
                    _nearConceptPhrases = new NearConceptPhraseRepository(_context);
                return _nearConceptPhrases;
            }
        }
        public IPlatformRepository Platforms
        {
            get
            {
                if (_platforms == null)
                    _platforms = new PlatformRepository(_context);
                return _platforms;
            }
        }
        public IPreferredLanguageRepository PreferredLanguages
        {
            get
            {
                if (_preferredLanguages == null)
                    _preferredLanguages = new PreferredLanguageRepository(_context);
                return _preferredLanguages;
            }
        }
        public IRawSearchRepository RawSearches
        {
            get
            {
                if (_rawSearches == null)
                    _rawSearches = new RawSearchRepository(_context);
                return _rawSearches;
            }
        }
        public ISettingRepository Settings
        {
            get
            {
                if (_settings == null)
                    _settings = new SettingRepository(_context);
                return _settings;
            }
        }
        public IPreferredSearchRepository PreferredSearches
        {
            get
            {
                if (_preferredSearches == null)
                {
                    _preferredSearches = new PreferredSearchRepository(_context);
                }
                return _preferredSearches;
            }
        }
        public RepositoryWrapper(ApplicationDbContext context) 
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

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
        private IActiveProjectRepository _activeProject;
        private IBadPhraseRepository _badPhrase;
        private IBadWordRepository _badWord;
        private IInstructorRepository _instructor;
        private ILanguageRepository _language;
        private INearConceptIdeaRepository _nearConceptIdea;
        private INearConceptPhraseRepository _nearConceptPhrase;
        private IPlatformRepository _platform;
        private IPreferredLanguageRepository _preferredLanguage;
        private IRawSearchRepository _rawSearch;
        private ISettingRepository _setting;
        private ITimeIndexRepository _timeIndex;

        public IActiveProjectRepository ActiveProject
        {
            get 
            {
                if (_activeProject == null)
                    _activeProject = new ActiveProjectRepository(_context);
                return _activeProject;
            }
        }
        public IBadPhraseRepository BadPhrase
        {
            get 
            {
                if (_badPhrase == null)
                    _badPhrase = new BadPhraseRepository(_context);
                return _badPhrase;
            }
        }
        public IBadWordRepository BadWord
        {
            get
            {
                if (_badWord == null)
                    _badWord = new BadWordRepository(_context);
                return _badWord;
            }
        }
        public IInstructorRepository Instructor
        {
            get
            {
                if (_instructor == null)
                    _instructor = new InstructorRepository(_context);
                return _instructor;
            }
        }
        public ILanguageRepository Language
        {
            get
            {
                if (_language == null)
                    _language = new LanguageRepository(_context);
                return _language;
            }
        }
        public INearConceptIdeaRepository NearConceptIdea
        {
            get
            {
                if (_nearConceptIdea == null)
                    _nearConceptIdea = new NearConceptIdeaRepository(_context);
                return _nearConceptIdea;
            }
        }
        public INearConceptPhraseRepository NearConceptPhrase
        {
            get
            {
                if (_nearConceptPhrase == null)
                    _nearConceptPhrase = new NearConceptPhraseRepository(_context);
                return _nearConceptPhrase;
            }
        }
        public IPlatformRepository Platform
        {
            get
            {
                if (_platform == null)
                    _platform = new PlatformRepository(_context);
                return _platform;
            }
        }
        public IPreferredLanguageRepository PreferredLanguage
        {
            get
            {
                if (_preferredLanguage == null)
                    _preferredLanguage = new PreferredLanguageRepository(_context);
                return _preferredLanguage;
            }
        }
        public IRawSearchRepository RawSearch
        {
            get
            {
                if (_rawSearch == null)
                    _rawSearch = new RawSearchRepository(_context);
                return _rawSearch;
            }
        }
        public ISettingRepository Setting
        {
            get
            {
                if (_setting == null)
                    _setting = new SettingRepository(_context);
                return _setting;
            }
        }
        public ITimeIndexRepository TimeIndex
        {
            get
            {
                if (_timeIndex == null)
                    _timeIndex = new TimeIndexRepository(_context);
                return _timeIndex;
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

using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHelperBot
{
    public class DbHandler
    {
        //todo tie in database
        private IRepositoryWrapper _repo;
        public DbHandler(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public async Task<List<List<string>>> GetListsSearchParameters(int day)
        {
            var preferredLanguageasync = Task.Run(()=> GetPreferredLanguage(day));
            var activeProjectasync = Task.Run(() => GetActiveProject(day));
            var languagesasync = Task.Run(() => GetLanguages());
            var badwordsasync = Task.Run(() => GetBadWords());
            var badphrasesasync = Task.Run(() => GetBadPhrases());
            var platformsasync = Task.Run(() => GetPlatforms());
            var preferredSearchasync = Task.Run(() => GetPreferredSearches());

            List<string> preferredLanguage = await preferredLanguageasync;
            List<string> activeProject = await activeProjectasync;
            List<string> languages = await languagesasync;
            List<string> badwords = await badwordsasync;
            List<string> badphrases = await badphrasesasync;
            List<string> platforms = await platformsasync;
            List<string> Preferredsearches = await preferredSearchasync;



            List<List<string>> searchHelperLists = new List<List<string>>() { preferredLanguage,activeProject,languages,badwords,badphrases,platforms,Preferredsearches};
            return searchHelperLists;
        }
        public async Task<List<string>> GetPreferredLanguage(int timeid)
        {
            var preferedLanguage =  _repo.PreferredLanguage.FindByCondition(p => p.TimeId == timeid);
            List<string> output = new List<string>();
            foreach (var item in preferedLanguage)
            {
                output.Add(item.LanguageName);
            }
            return output;
        }
        public async Task<List<string>> GetActiveProject(int timeid)
        {
            var preferedLanguage = _repo.ActiveProject.FindByCondition(p => p.TimeId == timeid);
            List<string> output = new List<string>();
            foreach (var item in preferedLanguage)
            {
                output.Add(item.ProjectType);
            }
            return output;
        }
        public async Task<List<string>> GetBadWords()
        {
            var Badwords = _repo.BadWord.FindAll();
            List<string> output = new List<string>();
            foreach (var item in Badwords)
            {
                output.Add(item.Word);
            }
            return output;
        }
        public async Task<List<string>> GetBadPhrases()
        {
            var badPhrases = _repo.BadPhrase.FindAll();
            List<string> output = new List<string>();
            foreach (var item in badPhrases)
            {
                output.Add(item.Phrase);
            }
            return output;
        }
        public async Task<List<string>> GetPlatforms()
        {
            var platforms = _repo.Platform.FindAll();
            List<string> output = new List<string>();
            foreach (var item in platforms)
            {
                output.Add(item.PlatformName);
            }
            return output;
        }
        public async Task<List<string>> GetLanguages()
        {
            var languages = _repo.Language.FindAll();
            List<string> output = new List<string>();
            foreach (var item in languages)
            {
                output.Add(item.LanguageName);
            }
            return output;
        }
        public async Task<List<string>> GetPreferredSearches()
        {
            var preferredSearches = _repo.PreferredSearch.FindAll();
            List<string> output = new List<string>();
            foreach (var item in preferredSearches)
            {
                output.Add(item.SearchName);
            }
            return output;
        }
        public async Task<Dictionary<string,string>> GetNearConcepts(int dayId)
        {
            var data = _repo.NearConceptIdea.FindByCondition(c => c.TimeId <= dayId||c.TimeId==0);
            Dictionary<string, string> output = new Dictionary<string, string>();
            foreach (var item in data)
            {
                var phrases = _repo.NearConceptPhrase.FindByCondition(p => p.ConceptID == item.NearConceptIdeaId);
                foreach (var phrase in phrases)
                {
                    output.Add(phrase.Phrase, item.ProperForm);
                }
            }
            return output;
        }

    }
}

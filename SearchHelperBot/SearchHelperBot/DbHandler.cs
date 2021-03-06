﻿using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHelperBot
{
    public class DbHandler
    {
        // member variables
        private IRepositoryWrapper _repo;

        // constructor
        public DbHandler(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // member methods
        public async Task<List<List<string>>> GetListsSearchParameters(int day)
        {
            var preferredLanguage = GetPreferredLanguage(day);
            var activeProject = GetActiveProject(day);
            var languages = GetLanguages();
            var badwords = GetBadWords();
            var badphrases = GetBadPhrases();
            var platforms = GetPlatforms();
            var preferredsearches = GetPreferredSearches();

            List<List<string>> searchHelperLists = new List<List<string>>() { preferredLanguage, activeProject, languages, badwords, badphrases, platforms, preferredsearches};
            return searchHelperLists;
        }
        public List<string> GetPreferredLanguage(int dayId)
        {
            var preferedLanguage =  _repo.PreferredLanguages.FindByCondition(p => p.Day >= dayId || p.Day == 0);
            List<string> output = new List<string>();
            foreach (var item in preferedLanguage)
            {
                output.Add(item.LanguageName);
            }
            return output;
        }
        public List<string> GetActiveProject(int dayId)
        {
            var preferedLanguage = _repo.ActiveProjects.FindByCondition(p => p.Day >= dayId || p.Day == 0);
            List<string> output = new List<string>();
            foreach (var item in preferedLanguage)
            {
                output.Add(item.ProjectType);
            }
            return output;
        }
        public List<string> GetBadWords()
        {
            var Badwords = _repo.BadWords.FindAll();
            List<string> output = new List<string>();
            foreach (var item in Badwords)
            {
                output.Add(item.Word);
            }
            return output;
        }
        public List<string> GetBadPhrases()
        {
            var badPhrases = _repo.BadPhrases.FindAll();
            List<string> output = new List<string>();
            foreach (var item in badPhrases)
            {
                output.Add(item.Phrase);
            }
            return output;
        }
        public List<string> GetPlatforms()
        {
            var platforms = _repo.Platforms.FindAll();
            List<string> output = new List<string>();
            foreach (var item in platforms)
            {
                output.Add(item.PlatformName);
            }
            return output;
        }
        public List<string> GetLanguages()
        {
            var languages = _repo.Languages.FindAll();
            List<string> output = new List<string>();
            foreach (var item in languages)
            {
                output.Add(item.LanguageName);
            }
            return output;
        }
        public List<string> GetPreferredSearches()
        {
            var preferredSearches = _repo.PreferredSearches.FindAll();
            List<string> output = new List<string>();
            foreach (var item in preferredSearches)
            {
                output.Add(item.SearchName);
            }
            return output;
        }
        public Dictionary<string,string> GetNearConcepts(int dayId)
        {
            var data = _repo.NearConceptIdeas.FindByCondition(c => c.Day >= dayId || c.Day == 0);
            Dictionary<string, string> output = new Dictionary<string, string>();
            foreach (var item in data)
            {
                var phrases = _repo.NearConceptPhrases.FindByCondition(p => p.ConceptId == item.NearConceptIdeaId);
                foreach (var phrase in phrases)
                {
                    output.Add(phrase.Phrase, item.ProperForm);
                }
            }
            return output;
        }
    }
}

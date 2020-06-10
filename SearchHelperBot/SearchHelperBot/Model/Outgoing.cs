using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHelperBot.Model
{
    public class Outgoing
    {
        public string Responsetype;
        public List<string> Searches;
        public List<ActiveProject> activeProjects;
        public List<PreferredLanguage> preferredLanguages;
        public List<PreferredSearch> preferredSearches;
        public List<Setting> settings;
        public List<BadWord> badWords;
        public List<BadPhrase> badPhrases;
        public List<NearConceptIdea> conceptideas;
        public List<NearConceptPhrase> conceptPhrases;
        public List<Language> languages;
        public List<Platform> platforms;
        public List<RawSearch> rawSearches;
    }
}

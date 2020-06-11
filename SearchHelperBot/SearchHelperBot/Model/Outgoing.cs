﻿using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHelperBot.Model
{
    // Contains lists of each kind of model and a List<string> for the search results 
    // so can create a selectlist on the frontend for user to choose what to edit or delete
    public class Outgoing
    {
        public string responseType;
        public List<string> searches;
        public List<ActiveProject> activeProjects;
        public List<BadPhrase> badPhrases;
        public List<BadWord> badWords;
        public List<Language> languages;
        public List<NearConceptIdea> nearConceptIdeas;
        public List<NearConceptPhrase> nearConceptPhrases;
        public List<Platform> platforms;
        public List<PreferredLanguage> preferredLanguages;
        public List<PreferredSearch> preferredSearches;
        public List<RawSearch> rawSearches;
        public List<Setting> settings;
    }
}

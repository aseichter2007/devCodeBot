using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;

namespace SearchHelperBot
{
    /// <summary>
    /// This is a search parameter filter that takes in a string and returns a string or array of strings with usefull keywords added and filler words removed
    /// </summary>
    /// <remarks>
    /// output methods are: 
    /// devCodeBot should instantiate with (string preferredLanguage, string activeProject) and give values relevant to the current student
    /// uses configutation file SearchHelperConfiguration.cs for parameters not passed in.
    /// Helpsearch(string input) ,
    /// finalSearchVariance(string input) 
    /// </remarks>
    class SearchHelper
    {
        string preferredLanguage;
        string activeProject;//the constructors are not using this very well. TODO: fix the not so necessary constructors.
        List<string> languages;
        List<string> badwords;
        List<string> badphrases;
        List<string> platforms;
        List<string> preferredSearches;
        Dictionary<string, string> nearConcepts;
        char splitchar = ' ';
        /// <summary>
        /// using values from SearchHelperConfiguration.cs
        /// </summary>
        public SearchHelper()
        {
            activeProject = SearchHelperConfiguration.activeProject;
            preferredLanguage = SearchHelperConfiguration.preferedLanguage;//the language students expect to work in the most during class.
            languages = SearchHelperConfiguration.languages;//stores programming languages that devCodeCamp students might be using.
            badwords = SearchHelperConfiguration.badwords;//stores words not useful in finding proper search results.
            badphrases = SearchHelperConfiguration.badphrases;//stores phrases not useful in finding proper search results.
            platforms = SearchHelperConfiguration.platforms;//stores names of platforms that students use.
            preferredSearches = SearchHelperConfiguration.preferredSearches;//stores list of preffered sites that students should land on
            nearConcepts = SearchHelperConfiguration.nearConcepts;//dictionary of close terms to be replaced
        }
        /// <summary>
        /// using your preferred language reccomendation and values from SearchHelperConfugurations.cs
        /// </summary>
        /// <param name="preferedLanguage"></param>
        public SearchHelper(string preferredLanguage)
        {
            this.preferredLanguage = preferredLanguage;
            activeProject = SearchHelperConfiguration.activeProject;
            languages = SearchHelperConfiguration.languages;//stores programming languages that devCodeCamp students might be using.
            badwords = SearchHelperConfiguration.badwords;//stores words not useful in finding proper search results.
            badphrases = SearchHelperConfiguration.badphrases;//stores phrases not useful in finding proper search results.
            platforms = SearchHelperConfiguration.platforms;//stores names of platforms that students use.
            preferredSearches = SearchHelperConfiguration.preferredSearches;//stores list of preffered sites that students should land on
            nearConcepts = SearchHelperConfiguration.nearConcepts;//dictionary of close terms to be replaced

        }
        /// <summary>
        /// using your preferredLanguage and your activeProject, other values from SearchHelperConfiguration.
        /// </summary>
        /// <param name="preferredLanguage"></param>
        /// <param name="activeProject"></param>
        public SearchHelper(string preferredLanguage, string activeProject)
        {
            this.preferredLanguage = preferredLanguage;
            this.activeProject = activeProject;
            languages = SearchHelperConfiguration.languages;//stores programming languages that devCodeCamp students might be using.
            badwords = SearchHelperConfiguration.badwords;//stores words not useful in finding proper search results.
            badphrases = SearchHelperConfiguration.badphrases;//stores phrases not useful in finding proper search results.
            platforms = SearchHelperConfiguration.platforms;//stores names of platforms that students use.
            preferredSearches = SearchHelperConfiguration.preferredSearches;//stores list of preffered sites that students should land on
            nearConcepts = SearchHelperConfiguration.nearConcepts;//dictionary of close terms to be replaced

        }
        /// <summary>
        ///This constructor is intended to use database storage. pull all relevant data from the database and send it in.
        /// </summary>
        /// <param name="preferredLanguage"></param>
        /// <param name="activeProject"></param>
        /// <param name="languages"></param>
        /// <param name="badwords"></param>
        /// <param name="badphrases"></param>
        /// <param name="platforms"></param>
        /// <param name="preferreddSearches"></param>
        /// <param name="nearConcepts"></param>
        public SearchHelper(string preferredLanguage, string activeProject, List<string> languages, List<string> badwords, List<string> badphrases, List<string> platforms, List<string> preferreddSearches, Dictionary<string, string> nearConcepts, char splitchar)
        {
            this.preferredLanguage = preferredLanguage;
            this.languages = languages;
            this.badwords = badwords;
            this.badphrases = badphrases;
            this.platforms = platforms;
            preferredSearches = preferreddSearches;
            this.nearConcepts = nearConcepts;
            this.activeProject = activeProject;
            this.splitchar = splitchar;
        }
        /// <summary>
        /// returns an array of optimized searches from input appended with each preferred search site appended
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<string> FinalSearchVariance(string input)
        {
            ////uncomment this for production
            input = HelpSearch(input);


            //outputs permutations of a string with items in the prefered search sites appended 
            List<string> output = new List<string>() { input };
            foreach (string preferredSite in preferredSearches)
            {
                output.Add(input + " " + preferredSite);
            }
            return output;
        }
        /// <summary>
        /// returs an optimzed websearch according to the specified configurations
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string HelpSearch(string input)
        {
            //performs all the methods in order to improve a user question into a better result
            string working = SearchCleaner(input.ToLower());//ceans badwords and badphrases from string
            working = NearConceptParse(working);
            if (activeProject != "")
            {
                working = Projects(working);//checks if a project type is defined and adds it if none are declared.
            }
            working = Language(working);//checks and adds preferred language if no lanuage specified
            return working;
        }
        public string Projects(string input)
        {
            {
                //determines if a language is specified and adds preferredLanguage to the final output if missing
                bool projectDefined = false;
                foreach (string item in platforms)
                {
                    if (input.Contains(item))
                    {
                        projectDefined = true;
                    }
                }
                string output = "";
                if (!projectDefined)
                {
                    output = input + " " + activeProject;
                }
                else
                {
                    output = input;
                }
                return output;
            }
        }
        public string NearConceptParse(string input)
        {
            string[] working = input.Split(splitchar);
            foreach (KeyValuePair<string, string> nearConcept in nearConcepts)
            {

                int location = PhraseLocate(working, nearConcept.Key);
                if (location != -1)
                {
                    working = PhraseRemover(location, working, nearConcept.Key);
                    working = PhraseInsert(location, working, nearConcept.Value);
                }
            }
            StringBuilder outputstring = new StringBuilder();
            foreach (string word in working)
            {
                outputstring.Append(word + " ");
            }
            string output = outputstring.ToString().Trim();
            return output;
        }
        public string SearchCleaner(string input)
        {
            //splits and reconstructs string after passing the split to WordAndPhraseHandler()
            string[] working = input.Split(splitchar);
            working = WordAndPhrasehandler(working);
            StringBuilder processed = new StringBuilder();
            foreach (string word in working)
            {
                processed.Append(word + " ");
            }
            string output = processed.ToString().Trim();
            return output;
        }
        public string Language(string input)
        {
            //determines if a language is specified and adds preferredLanguage to the final output if missing
            bool languageDefined = false;
            foreach (string item in languages)
            {
                if (input.Contains(item))
                {
                    languageDefined = true;
                }
            }
            string output = "";
            if (!languageDefined)
            {
                output = input + " " + preferredLanguage;
            }
            else
            {
                output = input;
            }
            return output;
        }
        public string[] WordAndPhrasehandler(string[] input)
        {
            //checks and removes bad words and phrases from input array using PhraseChecker() and PhraseRemover()
            string[] working = input;
            foreach (string badword in badwords)
            {
                int loc = PhraseLocate(working, badword);
                if (loc != -1)
                {
                    working = PhraseRemover(loc, working, badword);
                }
            }
            foreach (string badphrase in badphrases)
            {
                int loc = PhraseLocate(working, badphrase);
                if (loc != -1)
                {
                    working = PhraseRemover(loc, working, badphrase);
                }
            }
            return working;
        }
        public string[] PhraseRemover(int start, string[] removeFrom, string removePhrase)
        {
            //this whole funciton might be doable wih just 
            //removeFrom.ToString().Remove(removePhrase); or something similar
            string[] phrase = removePhrase.Split(' ');
            string[] check = removeFrom;
            do
            {
                List<string> removed = check.ToList();
                removed.RemoveRange(start, phrase.Length);
                check = removed.ToArray();
                start = PhraseLocate(check, removePhrase);
            } while (start != -1);



            return check;
        }
        public int PhraseLocate(string[] check, string containsPhrase)
        {
            //checks for the input phrase in the query array.       
            int output = -1;
            string[] phrase = containsPhrase.Split(' ');
            string working = "";
            for (int i = 0; i < check.Length; i++)
            {
                if (check[i] == phrase[0])
                {
                    if (i + phrase.Length <= check.Length)
                    {
                        StringBuilder thisphrase = new StringBuilder();
                        for (int ii = 0; ii < phrase.Length; ii++)
                        {
                            thisphrase.Append(check[i + ii] + " ");
                        }
                        working = thisphrase.ToString().Trim();
                    }
                    if (working == containsPhrase)
                    {
                        output = i;
                        break;
                    }
                }
            }
            return output;
        }
        public string[] PhraseInsert(int location, string[] insertinto, string PhraseInserted)
        {
            string[] Phrase = PhraseInserted.Split(' ');
            List<string> working = insertinto.ToList();
            working.InsertRange(location, Phrase);
            return working.ToArray();
        }
    }
}

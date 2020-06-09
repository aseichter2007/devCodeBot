using System;
using System.Collections.Generic;
using System.Text;

namespace SearchHelperBot
{
    /// <summary>
    /// contains the configuration lists for SearchHelper.cs
    /// add or remove terms from these lists to tune SearchHelper output.
    /// </summary>
    //this file has been seeded with examples and I could use help filling it out, I am kind of spent at the moment. 
    public static class SearchHelperConfiguration
    {
        public static string preferedLanguage = "c#";
        public static string activeProject = "asp.net mvc";
        public static List<string> languages = new List<string>
        {
            "c#",
            "javascript",
            "html",


        };
        public static List<string> badwords = new List<string>()
        {
            "i",
            "my",
            "am",
            "like",
            "maybe"




        };
        public static List<string> badphrases = new List<string>()
        {
            "trying to",
            "want to",
            "need to",



        };
        public static List<string> platforms = new List<string>()
        {
            ".net",
            ".net core",
            ".net mvc",
            "mvc",


        };
        public static List<string> preferredSearches = new List<string>()
        {
            "docs.microsoft.com",
            "stackoverflow",
            "w3schools",
        };
        //create concepts here for ease of entry for duplicate terms
        static string each = "loop over each value";
        static string index = "find index of";
        static string view = "mvc view";

        //remember that these values compare after the badwords and phrases are removed, and will not find matches if those words are used.
        public static Dictionary<string, string> nearConcepts = new Dictionary<string, string>()
        {
            { "get values in",each },
            { "search for value",each },
            { "get individual value",index },
            { "find location of",index },
            { "webpage in mvc" , view},


        };

    }
}

namespace SearchHelperBot.Model
{
    public class Incoming
    {
        public Search search { get; set; }
    }

    public class Search
    {
        public string role { get; set; }		// "student", "instructor"
        public string username { get; set; }
        public Request request { get; set; }
        public Add add { get; set; }
        public Edit edit { get; set; }
        public Remove remove { get; set; }
        public Isetting isetting { get; set; }
    }

    public class Request
    {
        public string type { get; set; }		// "activeprojects", "badphrases", "badwords", "languages", "nearconcepts", 
                                                // "nearconceptideas", nearconceptphrases", "platforms", "preferredlanguages", 
                                                // "preferredsearches", "rawsearches", "settings", "add", "edit", "remove"
        public int day { get; set; }
        public string search { get; set; }		// user search request
    }

    public class Add
    {
        public string type { get; set; }        // "activeprojects", "badphrases", "badwords", "languages", "nearconcepts", 
                                                // "nearconceptideas", nearconceptphrases", "platforms", "preferredlanguages", 
                                                // "preferredsearches", "rawsearches", "settings"
        public int id { get; set; }
        public string name { get; set; }
        public string matchTo { get; set; }     // NearConceptIdea to match to.
        public int conceptKey { get; set; }
        public int day { get; set; }
    }

    public class Edit
    {
        public string type { get; set; }    // "activeprojects", "badphrases", "badwords", "languages", "nearconcepts", 
                                            // "nearconceptideas", nearconceptphrases", "platforms", "preferredlanguages", 
                                            // "preferredsearches", "rawsearches", "settings"
        public int id { get; set; }
        public string originalname { get; set; }
        public string newname { get; set; }
        public string matchto { get; set; }
        public int conceptKey { get; set; }
        public int day { get; set; }
    }

    public class Remove
    {
        public string type { get; set; }    // "activeprojects", "badphrases", "badwords", "languages", "nearconcepts", 
                                            // "nearconceptideas", nearconceptphrases", "platforms", "preferredlanguages", 
                                            // "preferredsearches", "rawsearches", "settings"
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Isetting
    {
        public int id { get; set; }
        public bool set { get; set; }
    }
}
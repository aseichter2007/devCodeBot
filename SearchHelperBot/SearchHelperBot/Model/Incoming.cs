namespace SearchHelperBot.Model
{
    public class Incoming
    {
        public Search search { get; set; }
    }

    public class Search
    {
        public string role { get; set; }
        public string username { get; set; }
        public Request request { get; set; }
        public Add add { get; set; }
        public Edit edit { get; set; }
        public Remove remove { get; set; }
        public Setting setting { get; set; }
    }

    public class Request
    {
        public string type { get; set; }
        public int day { get; set; }
        public string search { get; set; }
    }

    public class Add
    {
        public string type { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string matchTo { get; set; }
        public string conceptKey { get; set; }
        public int day { get; set; }
    }

    public class Edit
    {
        public string type { get; set; }
        public int id { get; set; }
        public string originalname { get; set; }
        public string newname { get; set; }
        public string matchto { get; set; }
        public string conceptKey { get; set; }
        public int day { get; set; }
    }

    public class Remove
    {
        public string type { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Setting
    {
        public int id { get; set; }
        public bool set { get; set; }
    }
}
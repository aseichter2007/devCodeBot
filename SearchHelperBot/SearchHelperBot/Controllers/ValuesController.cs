using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;
using Repository.Models;
using SearchHelperBot.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SearchHelperBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // member variables
        private IRepositoryWrapper _repo;

        // constructor
        public ValuesController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // member methods
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()    //not used
        {
            return new string[] { "Get()", "Not Used" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{search}")]
        public  async Task<List<string>> Get(string search )    //we can get our data from the front end as a giant string with underscores as word dividers and return a processed string
        {                                                       //or we can get json
            int day = 0;    // will come with search when implemented
            char split = '_';//urls cannot contain spaces, this sets the split charachter in SearchHelper.cs
            List<string> processedSearches = await ProcessSearch(day, search, split);

            return processedSearches;
        }
                
        // POST api/<ValuesController>
        // POST will be our sole entrypoint and the incoming file will define what happens
        [HttpPost]
        public async Task<Outgoing> Post([FromBody] Incoming incoming)
        {
            if (incoming.search.role == "student")    // basic search
            {
                int day = incoming.search.request.day;
                string search = incoming.search.request.search;
                Outgoing searchResult = new Outgoing();

                // Searchhelper requires the split characher to be passed in so that it can work with both plaintest strings from post 
                // and url formatted string which cannot contain spaces.
                char split = ' ';
                searchResult.searches = await ProcessSearch(day, search, split);
                return searchResult;
            }
            else      //if not type student, it must be an instructor
            {
                if (incoming.search.request.type != "add" && incoming.search.request.type != "edit")
                {
                    return await PostGet(incoming);
                }
                else if (incoming.search.request.type == "add")
                {
                    return await PostAdd(incoming);
                }
                else if (incoming.search.request.type == "edit")// for request type edit, find the existing database entry, and update it with the incoming values. 
                {
                    if (incoming.search.edit.type == "badword")
                    {
                        BadWord badWord;//find in database and update values
                    }
                    else if (incoming.search.edit.type == "badphrase")
                    {

                    }
                    //contine for all types

                    Outgoing error3 = new Outgoing();
                    error3.responseType = "something went wrong in edit";
                    return error3;
                }

                Outgoing error2 = new Outgoing();
                error2.responseType = "something went wrong in request check";
                return error2;
            }
            //Outgoing error = new Outgoing();
            //error.Responsetype = "something went wrong in role test";
            //return error;           
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]       
        public void Put(int id, [FromBody] string value)        //not used
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)                              //not used
        {
        }

        private async Task<List<string>> ProcessSearch(int day, string search, char split)
        {
            DbHandler dbHandler = new DbHandler(_repo);
            Task<Dictionary<string, string>> searchHelperDictionaryasync = Task.Run(() => dbHandler.GetNearConcepts(day));
            Dictionary<string, string> searchHelperDictionary = await searchHelperDictionaryasync;
            Task<List<List<string>>> searchHelperListsasync = Task.Run(() => dbHandler.GetListsSearchParameters(day));
            List<List<string>> searchHelperLists = await searchHelperListsasync;
            SearchHelper searchHelper = new SearchHelper(searchHelperLists[0][0], searchHelperLists[1][0], searchHelperLists[2], 
                                                         searchHelperLists[3], searchHelperLists[4], searchHelperLists[5], 
                                                         searchHelperLists[6], searchHelperDictionary, split);

            List<string> optimizedSearches = searchHelper.FinalSearchVariance(search);
            return optimizedSearches;
        }

        private async Task<Outgoing> PostGet(Incoming incoming)
        {
            Outgoing outgoing = new Outgoing();

            outgoing.responseType = incoming.search.request.type;

            switch (incoming.search.request.type)
            {
                case "activeprojects":
                    outgoing.activeProjects = _repo.ActiveProjects.FindAll().ToList();
                    break;
                case "badphrases":
                    outgoing.badPhrases = _repo.BadPhrases.FindAll().ToList();
                    break;
                case "badwords":
                    outgoing.badWords = _repo.BadWords.FindAll().ToList();
                    break;
                case "instructors":
                    outgoing.instructors = _repo.Instructors.FindAll().ToList();
                    break;
                case "languages":
                    outgoing.languages = _repo.Languages.FindAll().ToList();
                    break;
                case "nearconcepts":
                    List<NearConcept> nearConcepts = new List<NearConcept>();
                    var nearConceptPhrases = _repo.NearConceptPhrases.FindAll().ToList();
                    foreach(var phrase in nearConceptPhrases)
                    {
                        NearConceptIdea nearConceptIdea = _repo.NearConceptIdeas.FindByCondition(i => i.NearConceptIdeaId == phrase.ConceptId).SingleOrDefault();
                        NearConcept nearConcept = new NearConcept();
                        nearConcept.Phrase = phrase.Phrase;
                        nearConcept.ProperForm = nearConceptIdea.ProperForm;
                        nearConcept.Day = nearConceptIdea.Day;
                        nearConcepts.Add(nearConcept);
                    }
                    outgoing.nearConcepts = nearConcepts;
                    break;
                case "platforms":
                    outgoing.platforms = _repo.Platforms.FindAll().ToList();
                    break;
                case "preferredlanguages":
                    outgoing.preferredLanguages = _repo.PreferredLanguages.FindAll().ToList();
                    break;
                case "preferredsearches":
                    outgoing.preferredSearches = _repo.PreferredSearches.FindAll().ToList();
                    break;
                case "rawsearches":
                    outgoing.rawSearches = _repo.RawSearches.FindAll().ToList();
                    break;
                case "settings":
                    outgoing.settings = _repo.Settings.FindAll().ToList();
                    break;
                default:
                    outgoing.responseType = "something went wrong in get";
                    break;
            }
            return outgoing;
        }

        private async Task<Outgoing> PostAdd(Incoming incoming)
        {
            switch (incoming.search.add.type)
            {
                case "activeprojects":
                    ActiveProject activeProject = new ActiveProject();
                    activeProject.ProjectType = incoming.search.add.name;
                    activeProject.Day = incoming.search.add.day;
                    _repo.ActiveProjects.Create(activeProject);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "badphrases":
                    BadPhrase badPhrase = new BadPhrase();
                    badPhrase.Phrase = incoming.search.add.name;
                    _repo.BadPhrases.Create(badPhrase);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "badwords":
                    BadWord badWord = new BadWord();
                    badWord.Word = incoming.search.add.name;
                    _repo.BadWords.Create(badWord);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "instructors":
                    Instructor instructor = new Instructor();
                    instructor.UserName = incoming.search.add.name;
                    _repo.Instructors.Create(instructor);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "languages":
                    Language language = new Language();
                    language.LanguageName = incoming.search.add.name;
                    _repo.Languages.Create(language);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "nearconcepts":
                    {
                        NearConceptIdea nearConceptIdea = new NearConceptIdea();
                        nearConceptIdea.ProperForm = incoming.search.add.name;
                        nearConceptIdea.Day = incoming.search.add.day;
                        _repo.NearConceptIdeas.Create(nearConceptIdea);
                        _repo.Save();
                        incoming.search.request.type = incoming.search.add.type;
                        return await PostGet(incoming);
                        break;
                    }
                case "nearconceptphrases":
                    {
                        NearConceptIdea nearConceptIdea = _repo.NearConceptIdeas.FindByCondition(c => c.ProperForm == incoming.search.add.matchTo).SingleOrDefault();
                        NearConceptPhrase nearConceptPhrase = new NearConceptPhrase();
                        if (nearConceptIdea == null)    // add new nearConceptIdea;
                        {
                            nearConceptIdea = new NearConceptIdea();
                            nearConceptIdea.ProperForm = incoming.search.add.matchTo;
                            nearConceptIdea.Day = incoming.search.add.day;
                            _repo.NearConceptIdeas.Create(nearConceptIdea);
                        }
                        nearConceptPhrase.Phrase = incoming.search.add.name;
                        nearConceptPhrase.ConceptId = nearConceptIdea.NearConceptIdeaId;
                        _repo.NearConceptPhrases.Create(nearConceptPhrase);
                        _repo.Save();
                        incoming.search.request.type = incoming.search.add.type;
                        return await PostGet(incoming);
                        break;
                    }
                case "platforms":
                    Platform platform = new Platform();
                    platform.PlatformName = incoming.search.add.name;
                    _repo.Platforms.Create(platform);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "preferredlanguages":
                    PreferredLanguage preferredLanguage = new PreferredLanguage();
                    preferredLanguage.LanguageName = incoming.search.add.name;
                    preferredLanguage.Day = incoming.search.add.day;
                    _repo.PreferredLanguages.Create(preferredLanguage);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "preferredsearches":
                    PreferredSearch preferredSearch = new PreferredSearch();
                    preferredSearch.SearchName = incoming.search.add.name;
                    _repo.PreferredSearches.Create(preferredSearch);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "rawsearches":
                    RawSearch rawSearch = new RawSearch();
                    rawSearch.StudentName = incoming.search.add.name;
                   // rawSearch.Search = incoming.search.add.;
                    _repo.RawSearches.Create(rawSearch);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "settings":
                    Setting setting = new Setting();
                    setting.SettingName = incoming.search.add.name;
                    //setting.Set = incoming.search.add.;
                    _repo.Settings.Create(setting);
                    _repo.Save();
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                default:
                    Outgoing outgoing = new Outgoing();
                    outgoing.responseType = "something went wrong in add";
                    return outgoing;
                    break;
            }
        }
    }
}
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
        public IEnumerable<string> Get()//naked get needs to return TODO: determine if this will ever be used
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{search}")]
        public  async Task<List<string>> Get(string search )//we can get our data from the front end as a giant string with underscores as word dividers and return a processed string
        {                                                   //or we can get json
            int day = 0;    // will come with search when implemented
            List<string> processedSearches = await ProcessSearch(day, search);

            return processedSearches;
        }
                
        // POST api/<ValuesController>
        // POST will be our sole entrypoint and the incoming file will define what happens
        [HttpPost]
        public async Task<Outgoing> Post([FromBody] Incoming incoming)
        {
            if (incoming.search.role=="student")    // basic search
            {
                int day = incoming.search.request.day;
                string search = incoming.search.request.search;
                Outgoing searchResult = new Outgoing();
                searchResult.searches= await ProcessSearch(day, search);
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private async Task<List<string>> ProcessSearch(int day, string search)
        {
            DbHandler dbHandler = new DbHandler(_repo);
            Task<Dictionary<string, string>> searchHelperDictionaryasync = Task.Run(() => dbHandler.GetNearConcepts(day));
            Dictionary<string, string> searchHelperDictionary = await searchHelperDictionaryasync;
            Task<List<List<string>>> searchHelperListsasync = Task.Run(() => dbHandler.GetListsSearchParameters(day));
            List<List<string>> searchHelperLists = await searchHelperListsasync;
            SearchHelper searchHelper = new SearchHelper(searchHelperLists[0][0], searchHelperLists[1][0], searchHelperLists[2], 
                                                         searchHelperLists[3], searchHelperLists[4], searchHelperLists[5], 
                                                         searchHelperLists[6], searchHelperDictionary);

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
                    break;
                case "languages":
                    outgoing.languages = _repo.Languages.FindAll().ToList();
                    break;
                case "nearconceptideas":
                    outgoing.nearConceptIdeas = _repo.NearConceptIdeas.FindAll().ToList();
                    break;
                case "nearconceptphrases":
                    outgoing.nearConceptPhrases = _repo.NearConceptPhrases.FindAll().ToList();
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
            Outgoing outgoing = new Outgoing();

            outgoing.responseType = incoming.search.request.type;

            switch (incoming.search.add.type)
            {
                case "activeprojects":
                    outgoing.activeProjects = _repo.ActiveProjects.FindAll().ToList();
                    break;
                case "badphrases":
                    BadPhrase badPhrase = new BadPhrase();
                    badPhrase.Phrase = incoming.search.add.name;
                    _repo.BadPhrases.Create(badPhrase);
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "badwords":
                    BadWord badWord = new BadWord();
                    badWord.Word = incoming.search.add.name;
                    _repo.BadWords.Create(badWord);
                    incoming.search.request.type = incoming.search.add.type;
                    return await PostGet(incoming);
                    break;
                case "instructors":
                    break;
                case "languages":
                    outgoing.languages = _repo.Languages.FindAll().ToList();
                    break;
                case "nearconceptideas":
                    outgoing.nearConceptIdeas = _repo.NearConceptIdeas.FindAll().ToList();
                    break;
                case "nearconceptphrases":
                    NearConceptIdea nearConceptIdea = _repo.NearConceptIdeas.FindByCondition(c => c.ProperForm == incoming.search.add.matchTo).SingleOrDefault();//until I am more sure how complex the modals will let things become, shy away from the ids for finding things
                    NearConceptPhrase nearConceptPhrase = new NearConceptPhrase();
                    if (nearConceptIdea == null)//not sure how this repo system will return. if it's empty we need to populate its values and add it to the database, then pull it again to get the key to pair the foreign key. 
                    {
                        nearConceptPhrase.Phrase = incoming.search.add.name;
                        nearConceptIdea.ProperForm = incoming.search.add.matchTo;//properform is the working string in nearconceptidea.  matchto will have the desired matched search.  I did a terrible job naming these things, sorry. Change them if you like but the keys have to match the json so I will need the exact changes. 
                    }
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
                    outgoing.responseType = "something went wrong in add";
                    break;
            }
            return outgoing;
        }
    }
}
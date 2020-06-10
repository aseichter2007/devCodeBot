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

        private IRepositoryWrapper _repo;
        public ValuesController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()//naked get needs to return TODO: determine if this will ever be used
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{search}")]
        public  async Task<List<string>> Get(string search )//we can get our data from the front end as a giant string with underscores as word dividers and return a processed string
        {//or we can get json
            int day = 0;
            List<string> processedSearches = await ProcessSearch(day, search);

            return processedSearches;
        }
        
        
        // POST api/<ValuesController>
        //post will be our sole entrypoint and the incoming file will define what happens
        [HttpPost]
        public async Task<Outgoing> Post([FromBody] Incoming incoming)//this is looking incredibly ugly and should probably break into a bunch of methods or go into its own class like I did DbHandler
        {
            if (incoming.search.role=="student")//check if incoming message is a basic search
            {
                int day = incoming.search.request.day;
                string search = incoming.search.request.search;
                Outgoing searchresult = new Outgoing();//the outgoing object contains lists of each kind of model and a List<string> for the search results, so that I can create a selectlist on the front side for them to choose what to edit or delete
                searchresult.Searches= await ProcessSearch(day, search);
                return searchresult;
            }
            else//if not type student, it must be an instructor
            {
                if (incoming.search.request.type == "badwords")//check request type and return a list of that type
                {
                    Outgoing badwordslist = new Outgoing();
                    badwordslist.Responsetype = "badwords";
                    badwordslist.badWords = _repo.BadWord.FindAll().ToList();
                }
                else if (incoming.search.request.type == "badphrases")
                {
                    //return list of badPhrases
                }
                //continue and serve each list type
                else if (incoming.search.request.type == "add")//request type can also be crud operations
                {
                    if (incoming.search.add.type=="badword")//the search.add.type tells you what type has been requested to be added
                    {
                        BadWord badWord = new BadWord();
                        badWord.Word = incoming.search.add.name;// I badwords and phrases just have a name or phrase. the id will be generated
                        _repo.BadWord.Create(badWord);
                        Outgoing outgoing = new Outgoing();//respond with a list of all badwords for review
                        outgoing.Responsetype = "badwords";
                        outgoing.badWords = _repo.BadWord.FindAll().ToList();
                        return outgoing;
                    }
                    else if (incoming.search.add.type == "badphrase")//I am kind of slopy on my cases, whether all lowercase or camel case is is best use in the json, if you decide it should be a particular way, let me know, we just need consistency
                    {
                        BadPhrase badPhrase = new BadPhrase();
                        badPhrase.Phrase = incoming.search.add.name;
                        _repo.BadPhrase.Create(badPhrase);
                        Outgoing outgoing = new Outgoing();
                        outgoing.Responsetype = "badphrases";
                        outgoing.badPhrases = _repo.BadPhrase.FindAll().ToList();
                        return outgoing;
                    }
                    if (incoming.search.add.type == "nearconceptphrase")//this one is the trickiest cause you need to link the right NearConceptIdea, only if it exists
                    {
                        NearConceptIdea nearConceptIdea = _repo.NearConceptIdea.FindByCondition(c => c.ProperForm == incoming.search.add.matchTo).SingleOrDefault();//until I am more sure how complex the modals will let things become, shy away from the ids for finding things
                        NearConceptPhrase nearConceptPhrase = new NearConceptPhrase();
                        if (nearConceptIdea == null)//not sure how this repo system will return. if it's empty we need to populate its values and add it to the database, then pull it again to get the key to pair the foreign key. 
                        {
                            nearConceptPhrase.Phrase = incoming.search.add.name;
                            nearConceptIdea.ProperForm = incoming.search.add.matchTo;//properform is the working string in nearconceptidea.  matchto will have the desired matched search.  I did a terrible job naming these things, sorry. Change them if you like but the keys have to match the json so I will need the exact changes. 
                        }

                    }
                    //continue for all types that can be added. I dont think we are using Instructors.cs at this time but until we are sure, dont remove it







                    Outgoing error3 = new Outgoing();
                    error3.Responsetype = "something went wrong in add";
                    return error3;
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
                    error3.Responsetype = "something went wrong in edit";
                    return error3;
                }
                else if (true)
                {

                }

                Outgoing error2 = new Outgoing();
                error2.Responsetype = "something went wrong in request check";
                return error2;
            }
            Outgoing error = new Outgoing();
            error.Responsetype = "something went wrong in role test";
            return error;           
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
        async Task<List<string>> ProcessSearch(int day, string search)
        {
            DbHandler dbHandler = new DbHandler(_repo);
            Task<List<List<string>>> searchHelperListsasync = Task.Run(() => dbHandler.GetListsSearchParameters(day));
            List<List<string>> searchHelperLists = await searchHelperListsasync;
            Task<Dictionary<string, string>> searchHelperDicitnoaryasync = Task.Run(() => dbHandler.GetNearConcepts(day));
            Dictionary<string, string> searchHelperDicitonary = await searchHelperDicitnoaryasync;
            SearchHelper searchHelper = new SearchHelper(searchHelperLists[0][0], searchHelperLists[1][0], searchHelperLists[2], searchHelperLists[3], searchHelperLists[4], searchHelperLists[5], searchHelperLists[6], searchHelperDicitonary);



            List<string> optimizedSearches = searchHelper.FinalSearchVariance(search);
            return optimizedSearches;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;

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
        [HttpGet("{day}")]
        public  async Task<List<string>> Get(int day )//we can get our data from the front end as a giant string with underscores as word dividers and return a processed string
        {//or we can get json
            DbHandler dbHandler = new DbHandler(_repo);
            Task<List<List<string>>> searchHelperListsasync = Task.Run(()=> dbHandler.GetListsSearchParameters(day));
            Task<Dictionary<string, string>> searchHelperDicitnoaryasync = Task.Run(() => dbHandler.GetNearConcepts(day));

            string search = "test search help I am trying to get out of a dumptster";
            List<List<string>> searchHelperLists = await searchHelperListsasync;
            Dictionary<string, string> searchHelperDicitonary = await searchHelperDicitnoaryasync;
            SearchHelper searchHelper = new SearchHelper(searchHelperLists[0][0], searchHelperLists[1][0], searchHelperLists[2], searchHelperLists[3], searchHelperLists[4], searchHelperLists[5],searchHelperLists[6],searchHelperDicitonary);

            List<string> optimizedSearches = searchHelper.FinalSearchVariance(search);
            return optimizedSearches;
        }
        
        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

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
    }
}

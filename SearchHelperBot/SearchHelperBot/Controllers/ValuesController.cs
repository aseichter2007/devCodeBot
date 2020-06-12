using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            int day = 0;        // will come with search when implemented
            char split = '_';   //urls cannot contain spaces, this sets the split charachter in SearchHelper.cs
            List<string> processedSearches = await ProcessSearch(day, search, split);

            return processedSearches;
        }
                
        // POST api/<ValuesController>
        // POST will be our sole entrypoint and the incoming file will define what happens
        [HttpPost]
        public async Task<string> Post([FromBody] Incoming incoming)//changed output to strings
        {
            Outgoing outgoing;

            if (incoming.search.role == "student")    // basic search
            {
                int day = incoming.search.request.day;
                string search = incoming.search.request.search;
                outgoing = new Outgoing();

                // Searchhelper requires the split characher to be passed in so that it can work with both plaintest strings from post 
                // and url formatted string which cannot contain spaces.
                char split = ' ';
                outgoing.responseType = "search";
                outgoing.searches = await ProcessSearch(day, search, split);
            }
            else      //if not type student, it must be an instructor - manage database
            {
                if (incoming.search.request.type == "add")
                {
                    outgoing = await PostAdd(incoming);
                }
                else if (incoming.search.request.type == "edit")
                {
                    outgoing = await PostPut(incoming);
                }
                else if (incoming.search.request.type == "remove")
                {
                    outgoing = await PostDelete(incoming);
                }
                else
                {
                    outgoing = await PostGet(incoming);
                }
            }
            string result = JsonConvert.SerializeObject(outgoing);
            return result;
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
                    outgoing.responseType = "activeprojects";
                    break;
                case "badphrases":
                    outgoing.badPhrases = _repo.BadPhrases.FindAll().ToList();
                    outgoing.responseType = "badphrases";

                    break;
                case "badwords":
                    outgoing.badWords = _repo.BadWords.FindAll().ToList();
                    outgoing.responseType = "badwords";

                    break;
                case "instructors":
                    outgoing.instructors = _repo.Instructors.FindAll().ToList();
                    outgoing.responseType = "instructors";

                    break;
                case "languages":
                    outgoing.languages = _repo.Languages.FindAll().ToList();
                    outgoing.responseType = "languages";

                    break;
                case "nearconceptideas":  // returns NearConceptIdeas in NearConcept form.
                    {
                        List<NearConcept> nearConcepts = new List<NearConcept>();
                        List<NearConceptIdea> nearConceptIdeas = _repo.NearConceptIdeas.FindAll().ToList();
                        foreach (var idea in nearConceptIdeas)
                        {
                            NearConcept nearConcept = new NearConcept();
                            nearConcept.Phrase = null;
                            nearConcept.ProperForm = idea.ProperForm;
                            nearConcept.Day = idea.Day;
                            nearConcepts.Add(nearConcept);
                        }
                        outgoing.nearConcepts = nearConcepts;
                        outgoing.responseType = "nearConceptideas";

                    }
                    break;
                case "nearconcepts":
                case "nearconceptphrases":  // NearConceptPhrases are returned with their NearConceptIdea.
                    {
                        List<NearConcept> nearConcepts = new List<NearConcept>();
                        var nearConceptPhrases = _repo.NearConceptPhrases.FindAll().ToList();
                        foreach (var phrase in nearConceptPhrases)
                        {
                            NearConceptIdea nearConceptIdea = _repo.NearConceptIdeas.FindByCondition(i => i.NearConceptIdeaId == phrase.ConceptId).SingleOrDefault();
                            NearConcept nearConcept = new NearConcept();
                            nearConcept.Phrase = phrase.Phrase;
                            nearConcept.ProperForm = nearConceptIdea.ProperForm;
                            nearConcept.Day = nearConceptIdea.Day;
                            nearConcepts.Add(nearConcept);
                        }
                        outgoing.nearConcepts = nearConcepts;
                        outgoing.responseType = "nearconceptPhrases";

                    }
                    break;
                case "platforms":
                    outgoing.platforms = _repo.Platforms.FindAll().ToList();
                    outgoing.responseType = "platforms";

                    break;
                case "preferredlanguages":
                    outgoing.preferredLanguages = _repo.PreferredLanguages.FindAll().ToList();
                    outgoing.responseType = "preferredlanguages";

                    break;
                case "preferredsearches":
                    outgoing.preferredSearches = _repo.PreferredSearches.FindAll().ToList();
                    outgoing.responseType = "preferredsearches";

                    break;
                case "rawsearches":
                    outgoing.rawSearches = _repo.RawSearches.FindAll().ToList();
                    outgoing.responseType = "rawsearches";

                    break;
                case "settings":
                    outgoing.settings = _repo.Settings.FindAll().ToList();
                    outgoing.responseType = "settings";

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
                    break;
                case "badphrases":
                    BadPhrase badPhrase = new BadPhrase();
                    badPhrase.Phrase = incoming.search.add.name;
                    _repo.BadPhrases.Create(badPhrase);
                    break;
                case "badwords":
                    BadWord badWord = new BadWord();
                    badWord.Word = incoming.search.add.name;
                    _repo.BadWords.Create(badWord);
                    break;
                case "instructors":
                    Instructor instructor = new Instructor();
                    instructor.UserName = incoming.search.add.name;
                    _repo.Instructors.Create(instructor);
                    break;
                case "languages":
                    Language language = new Language();
                    language.LanguageName = incoming.search.add.name;
                    _repo.Languages.Create(language);
                    break;
                case "nearconceptideas":        // NearConceptIdeas can be added by themselves.
                    {
                        NearConceptIdea nearConceptIdea = new NearConceptIdea();
                        nearConceptIdea.ProperForm = incoming.search.add.name;
                        nearConceptIdea.Day = incoming.search.add.day;
                        _repo.NearConceptIdeas.Create(nearConceptIdea);
                        break;
                    }
                case "nearconcepts":
                case "nearconceptphrases":      // NearConceptPhrases must be added with a NearConceptIdea.
                    {
                        NearConceptPhrase nearConceptPhrase = new NearConceptPhrase();
                        NearConceptIdea nearConceptIdea = _repo.NearConceptIdeas.FindByCondition(i => i.ProperForm == incoming.search.add.matchTo).SingleOrDefault();
                        if (nearConceptIdea == null)    // add new nearConceptIdea;
                        {
                            nearConceptIdea = new NearConceptIdea();
                            nearConceptIdea.ProperForm = incoming.search.add.matchTo;
                            nearConceptIdea.Day = incoming.search.add.day;
                            _repo.NearConceptIdeas.Create(nearConceptIdea);
                            _repo.Save();
                            // reload it to get the id.
                            nearConceptIdea = _repo.NearConceptIdeas.FindByCondition(i => i.ProperForm == incoming.search.add.matchTo).SingleOrDefault();
                        }
                        nearConceptPhrase.Phrase = incoming.search.add.name;
                        nearConceptPhrase.ConceptId = nearConceptIdea.NearConceptIdeaId;
                        _repo.NearConceptPhrases.Create(nearConceptPhrase);
                        break;
                    }
                case "platforms":
                    Platform platform = new Platform();
                    platform.PlatformName = incoming.search.add.name;
                    _repo.Platforms.Create(platform);
                    break;
                case "preferredlanguages":
                    PreferredLanguage preferredLanguage = new PreferredLanguage();
                    preferredLanguage.LanguageName = incoming.search.add.name;
                    preferredLanguage.Day = incoming.search.add.day;
                    _repo.PreferredLanguages.Create(preferredLanguage);
                    break;
                case "preferredsearches":
                    PreferredSearch preferredSearch = new PreferredSearch();
                    preferredSearch.SearchName = incoming.search.add.name;
                    _repo.PreferredSearches.Create(preferredSearch);
                    break;
                case "rawsearches":
                    RawSearch rawSearch = new RawSearch();
                    rawSearch.StudentName = incoming.search.username;
                    rawSearch.Search = incoming.search.add.name;
                    _repo.RawSearches.Create(rawSearch);
                    break;
                case "settings":
                    Setting setting = new Setting();
                    setting.SettingName = incoming.search.add.name;
                    setting.Set = incoming.search.setting.set;
                    _repo.Settings.Create(setting);
                    break;
                default:
                    return await PostError("something went wrong in add");
            }
            _repo.Save();
            incoming.search.request.type = incoming.search.add.type;
            return await PostGet(incoming);
        }

        private async Task<Outgoing> PostPut(Incoming incoming)
        {
            switch (incoming.search.edit.type)
            {
                case "activeprojects":
                    ActiveProject activeProject = _repo.ActiveProjects.FindByCondition(p => p.ActiveProjectId == incoming.search.edit.id).SingleOrDefault();
                    if (activeProject == null)
                    {
                        return await PostError("PostEdit(): activeProject id does not exist");
                    }
                    activeProject.ProjectType = incoming.search.edit.newname;
                    activeProject.Day = incoming.search.edit.day;
                    _repo.ActiveProjects.Update(activeProject);
                    break;
                case "badphrases":
                    BadPhrase badPhrase = _repo.BadPhrases.FindByCondition(p => p.BadPhraseId == incoming.search.edit.id).SingleOrDefault();
                    if (badPhrase == null)
                    {
                        return await PostError("PostEdit(): badPhrase id does not exist");
                    }
                    badPhrase.Phrase = incoming.search.edit.newname;
                    _repo.BadPhrases.Update(badPhrase);
                    break;
                case "badwords":
                    BadWord badWord = _repo.BadWords.FindByCondition(w => w.BadWordId == incoming.search.edit.id).SingleOrDefault();
                    if (badWord == null)
                    {
                        return await PostError("PostEdit(): badWord id does not exist");
                    }
                    badWord.Word = incoming.search.edit.newname;
                    _repo.BadWords.Update(badWord);
                    break;
                case "instructors":
                    Instructor instructor = _repo.Instructors.FindByCondition(i => i.InstructorId == incoming.search.edit.id).SingleOrDefault();
                    if (instructor == null)
                    {
                        return await PostError("PostEdit(): instructor id does not exist");
                    }
                    instructor.UserName = incoming.search.edit.newname;
                    _repo.Instructors.Update(instructor);
                    break;
                case "languages":
                    Language language = _repo.Languages.FindByCondition(l => l.LanguageId == incoming.search.edit.id).SingleOrDefault();
                    if (language == null)
                    {
                        return await PostError("PostEdit(): language id does not exist");
                    }
                    language.LanguageName = incoming.search.edit.newname;
                    _repo.Languages.Update(language);
                    break;
                case "nearconceptideas":        // NearConceptIdeas can be edited by themselves.
                    {
                        NearConceptIdea nearConceptIdea = _repo.NearConceptIdeas.FindByCondition(i => i.NearConceptIdeaId == incoming.search.edit.id).SingleOrDefault();
                        if (nearConceptIdea == null)
                        {
                            return await PostError("PostEdit(): nearConceptIdea id does not exist");
                        }
                        nearConceptIdea.ProperForm = incoming.search.edit.newname;
                        nearConceptIdea.Day = incoming.search.edit.day;
                        _repo.NearConceptIdeas.Update(nearConceptIdea);
                        break;
                    }
                case "nearconcepts":
                case "nearconceptphrases":      // NearConceptPhrases must be edited with a NearConceptIdea.
                    {
                        NearConceptPhrase nearConceptPhrase = _repo.NearConceptPhrases.FindByCondition(p => p.NearConceptPhraseId == incoming.search.edit.id).SingleOrDefault();
                        if (nearConceptPhrase == null)
                        {
                            return await PostError("PostEdit(): nearConceptPhrase id does not exist");
                        }
                        // Doesn't make sense to edit the NearConceptIdea since it may be tied to other NearConceptPhrases.
                        // If the edited NearConceptIdea doesn't already exist, it will be created.
                        NearConceptIdea nearConceptIdea = _repo.NearConceptIdeas.FindByCondition(i => i.ProperForm == incoming.search.edit.matchto).SingleOrDefault();
                        if (nearConceptIdea == null)    // add new nearConceptIdea;
                        {
                            nearConceptIdea = new NearConceptIdea();
                            nearConceptIdea.ProperForm = incoming.search.edit.matchto;
                            nearConceptIdea.Day = incoming.search.edit.day;
                            _repo.NearConceptIdeas.Create(nearConceptIdea);
                            _repo.Save();
                            // reload it to get the id.
                            nearConceptIdea = _repo.NearConceptIdeas.FindByCondition(i => i.ProperForm == incoming.search.edit.matchto).SingleOrDefault();
                        }
                        nearConceptPhrase.Phrase = incoming.search.edit.newname;
                        nearConceptPhrase.ConceptId = nearConceptIdea.NearConceptIdeaId;
                        _repo.NearConceptPhrases.Update(nearConceptPhrase);
                        break;
                    }
                case "platforms":
                    Platform platform = _repo.Platforms.FindByCondition(p => p.PlatformId == incoming.search.edit.id).SingleOrDefault();
                    if (platform == null)
                    {
                        return await PostError("PostEdit(): platform id does not exist");
                    }
                    platform.PlatformName = incoming.search.edit.newname;
                    _repo.Platforms.Update(platform);
                    break;
                case "preferredlanguages":
                    PreferredLanguage preferredLanguage = _repo.PreferredLanguages.FindByCondition(p => p.PreferredLanguageId == incoming.search.edit.id).SingleOrDefault();
                    if (preferredLanguage == null)
                    {
                        return await PostError("PostEdit(): preferredLangauge id does not exist");
                    }
                    preferredLanguage.LanguageName = incoming.search.edit.newname;
                    preferredLanguage.Day = incoming.search.edit.day;
                    _repo.PreferredLanguages.Update(preferredLanguage);
                    break;
                case "preferredsearches":
                    PreferredSearch preferredSearch = _repo.PreferredSearches.FindByCondition(p => p.PreferredSearchId == incoming.search.edit.id).SingleOrDefault();
                    if (preferredSearch == null)
                    {
                        return await PostError("PostEdit(): preferredSearch id does not exist");
                    }
                    preferredSearch.SearchName = incoming.search.edit.newname;
                    _repo.PreferredSearches.Update(preferredSearch);
                    break;
                case "rawsearches":
                    RawSearch rawSearch = _repo.RawSearches.FindByCondition(s => s.RawSearchId == incoming.search.edit.id).SingleOrDefault();
                    if (rawSearch == null)
                    {
                        return await PostError("PostEdit(): rawSearch id does not exist");
                    }
                    rawSearch.StudentName = incoming.search.username;
                    rawSearch.Search = incoming.search.edit.newname;
                    _repo.RawSearches.Update(rawSearch);
                    break;
                case "settings":
                    Setting setting = _repo.Settings.FindByCondition(s => s.SettingId == incoming.search.edit.id).SingleOrDefault();
                    if (setting == null)
                    {
                        return await PostError("PostEdit(): setting id does not exist");
                    }
                    setting.SettingName = incoming.search.edit.newname;
                    setting.Set = incoming.search.setting.set;
                    _repo.Settings.Update(setting);
                    break;
                default:
                    return await PostError("something went wrong in edit");
            }
            _repo.Save();
            incoming.search.request.type = incoming.search.edit.type;
            return await PostGet(incoming);
        }

        private async Task<Outgoing> PostDelete(Incoming incoming)
        {
            switch (incoming.search.remove.type)
            {
                case "activeprojects":
                    ActiveProject activeProject = _repo.ActiveProjects.FindByCondition(p => p.ActiveProjectId == incoming.search.remove.id).SingleOrDefault();
                    if (activeProject == null)
                    {
                        return await PostError("PostRemove(): activeProject id does not exist");
                    }
                    _repo.ActiveProjects.Delete(activeProject);
                    break;
                case "badphrases":
                    BadPhrase badPhrase = _repo.BadPhrases.FindByCondition(p => p.BadPhraseId == incoming.search.remove.id).SingleOrDefault();
                    if (badPhrase == null)
                    {
                        return await PostError("PostRemove(): badPhrase id does not exist");
                    }
                    _repo.BadPhrases.Delete(badPhrase);
                    break;
                case "badwords":
                    BadWord badWord = _repo.BadWords.FindByCondition(w => w.BadWordId == incoming.search.remove.id).SingleOrDefault();
                    if (badWord == null)
                    {
                        return await PostError("PostRemove(): badWord id does not exist");
                    }
                    _repo.BadWords.Delete(badWord);
                    break;
                case "instructors":
                    Instructor instructor = _repo.Instructors.FindByCondition(i => i.InstructorId == incoming.search.remove.id).SingleOrDefault();
                    if (instructor == null)
                    {
                        return await PostError("PostRemove(): instructor id does not exist");
                    }
                    _repo.Instructors.Delete(instructor);
                    break;
                case "languages":
                    Language language = _repo.Languages.FindByCondition(l => l.LanguageId == incoming.search.remove.id).SingleOrDefault();
                    if (language == null)
                    {
                        return await PostError("PostRemove(): language id does not exist");
                    }
                    _repo.Languages.Delete(language);
                    break;
                case "nearconceptideas":        // NearConceptIdeas cannot be deleted.
                    break;
                case "nearconcepts":
                case "nearconceptphrases":      // NearConceptPhrases can be deleted.  The linked NearConceptIdea is not deleted.
                    NearConceptPhrase nearConceptPhrase = _repo.NearConceptPhrases.FindByCondition(p => p.NearConceptPhraseId == incoming.search.remove.id).SingleOrDefault();
                    if (nearConceptPhrase == null)
                    {
                        return await PostError("PostRemove(): nearConceptPhrase id does not exist");
                    }
                    _repo.NearConceptPhrases.Delete(nearConceptPhrase);
                    break;
                case "platforms":
                    Platform platform = _repo.Platforms.FindByCondition(p => p.PlatformId == incoming.search.remove.id).SingleOrDefault();
                    if (platform == null)
                    {
                        return await PostError("PostRemove(): platform id does not exist");
                    }
                    _repo.Platforms.Delete(platform);
                    break;
                case "preferredlanguages":
                    PreferredLanguage preferredLanguage = _repo.PreferredLanguages.FindByCondition(p => p.PreferredLanguageId == incoming.search.remove.id).SingleOrDefault();
                    if (preferredLanguage == null)
                    {
                        return await PostError("PostRemove(): preferredLangauge id does not exist");
                    }
                    _repo.PreferredLanguages.Delete(preferredLanguage);
                    break;
                case "preferredsearches":
                    PreferredSearch preferredSearch = _repo.PreferredSearches.FindByCondition(p => p.PreferredSearchId == incoming.search.remove.id).SingleOrDefault();
                    if (preferredSearch == null)
                    {
                        return await PostError("PostRemove(): preferredSearch id does not exist");
                    }
                    _repo.PreferredSearches.Delete(preferredSearch);
                    break;
                case "rawsearches":
                    RawSearch rawSearch = _repo.RawSearches.FindByCondition(s => s.RawSearchId == incoming.search.remove.id).SingleOrDefault();
                    if (rawSearch == null)
                    {
                        return await PostError("PostRemove(): rawSearch id does not exist");
                    }
                    _repo.RawSearches.Delete(rawSearch);
                    break;
                case "settings":
                    Setting setting = _repo.Settings.FindByCondition(s => s.SettingId == incoming.search.remove.id).SingleOrDefault();
                    if (setting == null)
                    {
                        return await PostError("PostRemove(): setting id does not exist");
                    }
                    _repo.Settings.Delete(setting);
                    break;
                default:
                    return await PostError("something went wrong in delete");
            }
            _repo.Save();
            incoming.search.request.type = incoming.search.remove.type;
            return await PostGet(incoming);
        }

        private async Task<Outgoing> PostError(string error)
        {
            Outgoing outgoing = new Outgoing();
            outgoing.responseType = error;
            return outgoing;
        }
    }
}
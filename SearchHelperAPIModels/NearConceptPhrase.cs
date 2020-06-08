using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISample.Models
{
    public class NearConceptPhrase
    {
        public int NearConceptPhraseId { get; set; }
        [ForeignKey("NearConceptIdea")]
        public int ConceptID { get; set; }
        NearConceptIdea NearConceptIdea { get; set; }
        public string Phrase { get; set; }

    }
}

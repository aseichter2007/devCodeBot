using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class NearConceptPhrase
    {
        public int NearConceptPhraseId { get; set; }
        public string Phrase { get; set; }

        [ForeignKey("NearConceptIdea")]
        public int ConceptId { get; set; }
        NearConceptIdea NearConceptIdea { get; set; }
    }
}

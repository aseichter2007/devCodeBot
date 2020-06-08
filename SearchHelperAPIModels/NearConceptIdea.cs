using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISample.Models
{
    public class NearConceptIdea
    {
        public int NearConceptIdeaId { get; set; }
        public string Title { get; set; }
        public string ProperForm { get; set; }
        [ForeignKey("TimeIndex")]
        public int TimeId { get; set; }
        TimeIndex TimeIndex { get; set; }
    }
}

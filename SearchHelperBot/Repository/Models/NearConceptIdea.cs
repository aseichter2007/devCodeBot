using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class NearConceptIdea
    {
        public int NearConceptIdeaId { get; set; }
        public string Title { get; set; }
        public string ProperForm { get; set; }
        public int Day { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISample.Models
{
    public class PreferredLanguage
    {
        public int PreferredLanguageId { get; set; }
        public string LanguageName { get; set; }
        [ForeignKey("TimeIndex")]
        public int TimeId { get; set; }
        TimeIndex TimeIndex { get; set; }

    }
}

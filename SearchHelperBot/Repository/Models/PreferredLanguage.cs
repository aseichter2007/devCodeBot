using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class PreferredLanguage
    {
        public int PreferredLanguageId { get; set; }
        public string LanguageName { get; set; }
        public int Day { get; set; }
    }
}

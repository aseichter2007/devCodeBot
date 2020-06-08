using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPISample.Models
{
    public class ActiveProject
    {
        public int ActiveProjectId { get; set; }
        public string ProjectType { get; set; }
        [ForeignKey("TimeIndex")]
        public int TimeId { get; set; }
        TimeIndex TimeIndex { get; set; }
    }
}

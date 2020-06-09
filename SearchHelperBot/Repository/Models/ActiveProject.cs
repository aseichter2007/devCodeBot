using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Models
{
    public class ActiveProject
    {
        public int ActiveProjectId { get; set; }
        public string ProjectType { get; set; }

        [ForeignKey("TimeIndex")]
        public int TimeId { get; set; }
        public TimeIndex TimeIndex { get; set; }
    }
}

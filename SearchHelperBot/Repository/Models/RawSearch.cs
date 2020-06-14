using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class RawSearch
    {
        public int RawSearchId { get; set; }
        public string StudentName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Search { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class SearchRequest
    {
        public bool SearchOrganization { get; set; }
        public bool SearchTeam { get; set; }
        public bool SearchProject { get; set; }
        public bool SearchProfile { get; set; }
        public string Name { get; set; }
    }
}
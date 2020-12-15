using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class SearchResults
    {
        public List<Organization> Organizations { get; set; } = null;
        public List<Profile> Profiles { get; set; } = null;
        public List<Project> Projects { get; set; } = null;
        public List<Team> Teams { get; set; } = null;
    }
}
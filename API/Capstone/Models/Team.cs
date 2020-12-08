using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Team
    {
        public int teamId { get; set; }
        public string teamName { get; set; }
        public string teamImage { get; set; }
        public string teamBio { get; set; }
        public int teamZip { get; set; }
        public string teamCity { get; set; }
        public string teamState { get; set; }
        public string teamContactEmail { get; set; }
        public List<Profile> teamMembers { get; set; }

    }
}

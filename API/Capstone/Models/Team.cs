using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public int CreatedBy { get; set; }
        public string TeamName { get; set; }
        public string TeamImage { get; set; }
        public string TeamBio { get; set; }
        public int TeamZip { get; set; }
        public string TeamCity { get; set; }
        public string TeamState { get; set; }
        public string TeamContactEmail { get; set; }
        public List<Profile> TeamMembers { get; set; }
    }
}
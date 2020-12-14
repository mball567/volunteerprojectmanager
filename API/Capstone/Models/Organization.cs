using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Organization
    {
        public int OrgId { get; set; }
        public int UserId { get; set; }
        public string OrgName { get; set; }
        public string OrgImage { get; set; }
        public string OrgBio { get; set; }
        public int OrgZip { get; set; }
        public string OrgCity { get; set; }
        public string OrgState { get; set; }
        public string OrgContactEmail { get; set; }
        public int[] OrgCauses { get; set; }
        public string[] OrgCauseNames { get; set; }
    }
}
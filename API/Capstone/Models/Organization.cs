using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Organization
    {
        public int orgId { get; set; }
        public int userId { get; set; }
        public string orgName { get; set; }
        public string orgImage { get; set; }
        public string orgBio { get; set; }
        public int orgZip { get; set; }
        public string orgCity { get; set; }
        public string orgState { get; set; }
        public string orgContactEmail { get; set; }
    }
}

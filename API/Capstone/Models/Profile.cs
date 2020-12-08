using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Profile
    {
        public int profileId { get; set; }
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string profileImage { get; set; }
        public string bio { get; set; }
        public int profZip { get; set; }
        public string profCity { get; set; }
        public string profState { get; set; }
        //public List<string> causes { get; set; }
        public string profContactEmail { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Project
    {
        public int projId { get; set; }
        public int orgId { get; set; }
        public string projName { get; set; }
        public string projDesc { get; set; }
        public string projImage { get; set; }
        public int projZip { get; set; }
        public string projCity { get; set; }
        public string projState { get; set; }
        public int projWorkingHours { get; set; }
        public string projContactEmail { get; set; }
    }
}

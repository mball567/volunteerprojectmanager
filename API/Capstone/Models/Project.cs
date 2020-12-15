using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Project
    {
        public int ProjId { get; set; }
        public int UserId { get; set; }
        public int ProfId { get; set; }
        public string ProjName { get; set; }
        public string ProjDesc { get; set; }
        public string ProjImage { get; set; }
        public int ProjZip { get; set; }
        public string ProjCity { get; set; }
        public string ProjState { get; set; }
        public int ProjWorkingHours { get; set; }
        public string ProjContactEmail { get; set; }
        public int[] ProjCauses { get; set; }
        public string[] ProjCauseNames { get; set; }
    }
}
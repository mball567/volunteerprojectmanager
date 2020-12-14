using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Bio { get; set; }
        public int ProfZip { get; set; }
        public string ProfCity { get; set; }
        public string ProfState { get; set; }
        public int[] ProfCauses { get; set; }
        public string ProfContactEmail { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Event
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int ProjId { get; set; }
        public string EventName { get; set; }
        public string EventDesc { get; set; }
        public int EventZip { get; set; }
        public string EventCity { get; set; }
        public string EventState { get; set; }
        public string EventStartTime { get; set; }
        public string EventEndTime { get; set; }
        public string EventDate { get; set; }
        public int EventWorkingHours { get; set; }
        public string EventContactEmail { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceCheckerSystem.Models
{
    public class Meeting
    {
        public int ID { get; set; }
        public string MeetingTimes { get; set; }
        public DateTime MeetingDay { get; set; }
        public string MeetingLocation { get; set; }
    }
}

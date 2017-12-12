using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceCheckerSystem.Models
{
    public class AttendMeeting
    {
        public int ID { get; set; }
        public int MeetingID { get; set; }
        public int StudentID { get; set; }
        public bool Attend { get; set; }
    }
}

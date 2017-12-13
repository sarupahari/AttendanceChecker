using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceCheckerSystem.Models
{
    public class StudentSummaryViewModel
    {
        public Student Student { get; set; }
        //public Meeting MeetingDay { get; set;}
        public AttendMeeting Attendance { get; set; }
        //public IEnumerable<AttendMeeting> Attend { get; set; }
        //public int TimesPresent { get; set; }
        //public int TimesAbsent { get; set; } 
        public int AttendancePercentage;
        //public int TimesAbsent { get { return Attend.Count(att => att.Attend == false); } }


    }
}

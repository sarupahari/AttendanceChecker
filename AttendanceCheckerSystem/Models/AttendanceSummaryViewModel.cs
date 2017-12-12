using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceCheckerSystem.Models
{
    public class AttendanceSummaryViewModel
    {
        public Student Student { get; set; }
       
        public AttendMeeting Attendance { get; set; }
       
        //public int TimesPresent { get; set; }
        //public int TimesAbsent { get; set; }
        //public int AttendancePercentage { get; set; }
        //public int TimesAbsent { get { return Attend.Count(att => att.Attend == false); } }
    }
}

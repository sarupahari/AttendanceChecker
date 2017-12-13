using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceCheckerSystem.Models
{
    public class ClassSummaryViewModel
    {
        public Student Student { get; set; }
        public AttendMeeting Attendance { get; set; }
        public int AttendancePercentage { get; set; }
        //public List<SummaryList> _returnList { get; set; }
        public int TimesPresent { get; set; }
        public int TimesAbsent { get; set; }
    }
    public class SummaryList
    {

        
        public int PresentPercentage { get; set; }
        public int StudentInfo { get; set; }
    }
}
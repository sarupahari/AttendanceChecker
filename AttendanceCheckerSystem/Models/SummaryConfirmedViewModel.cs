using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceCheckerSystem.Models
{
    public class SummaryConfirmedViewModel
    {
        public string StudentInfo { get; set; }
        public int TimesPresent { get; set; }
        public int TimesAbsent { get; set; }
    }
}

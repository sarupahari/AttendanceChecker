using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceCheckerSystem.Models
{
    public class AttendanceDetailViewModel
    {
        public Student Student { get; set; }
        public AttendMeeting Attend { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendanceCheckerSystem.Data;
using AttendanceCheckerSystem.Models;

namespace AttendanceCheckerSystem.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            var days = await _context.Meetings.ToListAsync();
            var sortdays = days.OrderBy(d => d.MeetingDay);
            return View(sortdays);
        }

        public IActionResult SearchStudents()
        {
            return View();
        }
        public async Task<IActionResult> SearchResults(SearchInputModel search)
        {
            var students = await _context.Students.ToListAsync();
            var filtered = students.Where(s => s.LastName == search.SearchLastName ||
                                            s.FirstMidName == search.SearchFirstName);
            return View(filtered);
        }

        [HttpPost, ActionName("Attend")]
        public IActionResult StudentAttends(AttendanceViewModel vm)
        {

            var selectedList = _context.AttendMeeting.Where(m => m.MeetingID == vm.MeetingID &&
                                                             m.StudentID == vm.StudentID);

            var selected = _context.AttendMeeting.Find(selectedList.First().ID);

            selected.Attend = true;

            _context.SaveChanges();

            return RedirectToAction("StudentAttendanceConfirmed", vm);
            //return View("StudentAttendanceConfirmed", selected);
        }

        public async Task<IActionResult> StudentAttendanceConfirmed(AttendanceViewModel attended)
        {
            var days = await _context.Meetings.ToListAsync();
            var students = await _context.Students.ToListAsync();

            var meetings = days.OrderBy(d => d.MeetingDay);
            ViewData["MeetingList"] = meetings;

            var studentsSorted = students.OrderBy(s => s.LastName);
            ViewData["StudentList"] = studentsSorted;

            var student = students.Where(s => s.ID == attended.StudentID).First();
            var meeting = meetings.Where(m => m.ID == attended.MeetingID).First();

            StudentAttendanceConfirmedViewModel vm = new StudentAttendanceConfirmedViewModel
            {
                StudentName = student.LastName + ", " + student.FirstMidName,
                DateAttended = meeting.MeetingDay
            };
            return View(vm);
        }
        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .SingleOrDefaultAsync(m => m.ID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            ViewData["MeetingInfo"] = meeting.MeetingDay.ToLongDateString();


            var model = _context.Students
                .Join(_context.AttendMeeting,
                      s => s.ID,
                      a => a.StudentID,
                      ((student, attend) => new AttendanceDetailViewModel { Student = student, Attend = attend }))
                .Where(m => m.Attend.MeetingID == meeting.ID)
                .OrderBy(m => m.Student.LastName);

            return View(model);
        }

        //Detail for  individual student
        //list all the dates in the semester and their status
        public async Task<IActionResult> StudentSummary(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .SingleOrDefaultAsync(s => s.ID == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["StudentInfo"] = student.ID + " " + student.LastName + ", " + student.FirstMidName;
            ViewData["StudentPhoto"] = student.BuffIDPhoto;

            var model = _context.Meetings
                .Join(_context.AttendMeeting,
                      m => m.ID,
                      a => a.MeetingID,
                     ((meeting, attend) => new StudentSummaryDetailViewModel { Meeting = meeting, Attend = attend }))
               .Where(s => s.Attend.StudentID == student.ID)
               .OrderBy(m => m.Meeting.MeetingDay);

            return View(model);
        }

        //Summary of the Attendance
        public async Task<IActionResult> AttendanceSummary(SummaryConfirmedViewModel am)
        {
            IEnumerable<AttendMeeting> attend = await _context.AttendMeeting.ToListAsync();
            IEnumerable<Student> student = await _context.Students.ToListAsync();
            //var attend = await _context.AttendMeeting.ToListAsync();
            //var student = await _context.Students.ToListAsync();

            //var present = (from a in attend
            //               where a.Attend == true
            //               select a).Count();
            //ViewData["TotalPresent"] = present;
            //var absent = (from a in attend
            //              where a.Attend == false
            //              select a).Count();
            //ViewData["TotalAbsent"] = absent;           
            //return View(asm);


            var present = 0;
            var absent = 0;
             foreach(var s in student)
            {                
                foreach (var a in attend)
                {
                    //var studentin = attend.Where(st => st.ID == attend.Attend);
                     present = attend.Where(att => att.Attend == true).Count();
                     absent = attend.Where(att => att.Attend == false).Count();                  

                }             
            }
            ViewData["TimesPresent"] = present;
            ViewData["TimesAbsent"] = absent;                   
            return View(am);
        }



        // GET: Meetings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MeetingTimes,MeetingDay,MeetingLocation")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings.SingleOrDefaultAsync(m => m.ID == id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MeetingTimes,MeetingDay,MeetingLocation")] Meeting meeting)
        {
            if (id != meeting.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .SingleOrDefaultAsync(m => m.ID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meeting = await _context.Meetings.SingleOrDefaultAsync(m => m.ID == id);
            _context.Meetings.Remove(meeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.ID == id);
        }
    }
}

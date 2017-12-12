using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AttendanceCheckerSystem.Models;
using AttendanceCheckerSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCheckerSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var days = await _context.Meetings.ToListAsync();
            var students = await _context.Students.ToListAsync();

            var meetings = days.OrderBy(d => d.MeetingDay);
            ViewData["MeetingList"] = meetings;

            var studentsSorted = students.OrderBy(s => s.LastName);
            ViewData["StudentList"] = studentsSorted;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

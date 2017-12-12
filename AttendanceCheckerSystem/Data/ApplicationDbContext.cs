using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AttendanceCheckerSystem.Models;

namespace AttendanceCheckerSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AttendanceCheckerSystem.Models.Student> Students { get; set; }
        public DbSet<AttendanceCheckerSystem.Models.Meeting> Meetings { get; set; }
        public DbSet<AttendanceCheckerSystem.Models.AttendMeeting> AttendMeeting { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Student>().ToTable("Student");
            builder.Entity<Meeting>().ToTable("Meeting");
            builder.Entity<AttendMeeting>().ToTable("AttendMeeting");
        }
    }
}

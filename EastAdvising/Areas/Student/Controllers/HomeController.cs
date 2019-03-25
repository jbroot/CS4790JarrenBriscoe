using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EastAdvising.Models;
using EastAdvising.Data;
using Microsoft.EntityFrameworkCore;
using EastAdvising.Extensions;
using Microsoft.AspNetCore.Hosting.Internal;
using EastAdvising.Models.ViewModels;

namespace EastAdvising.Controllers
{
    [Area("Student")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnvironment;
        [BindProperty]
        public AppointmentViewModels AppointmentVM { get; set; }

        public HomeController(ApplicationDbContext db, HostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            AppointmentVM = new AppointmentViewModels()
            {
                Advisor = _db.Advisor.ToList(),
                Service = _db.Service.ToList(),
                Student = new Models.Student(),
                Location = _db.Location.ToList(),
                Availability = new Models.Availability(),
                Appointment = new Models.Appointment()
            };

        }
        public async Task<IActionResult> Index()
        {
            var collegeList = await _db.College.Include(m => m.Departments)
                                                .ToListAsync();

            return View(collegeList);
        }


        public async Task<IActionResult> CollegeDetails(int id)
        {
            var collegeDetails = await _db.College.Include(m => m.Departments)
                                                .Where(m => m.CollegeId == id)
                                                .FirstOrDefaultAsync();
            return View(collegeDetails);
        }

        public async Task<IActionResult> MajorDetails(int id)
        {
            var majorDetails = await _db.Major.Include(m => m.Department)
                                                .Where(m => m.Department.DepartmentId == id)
                                                .ToListAsync();
            return View(majorDetails);
        }
        public async Task<IActionResult> AdvisorDetails(int id)
        {
            var AdvisorDetails = await _db.Advisor.Include(m => m.Appointments)
                                                .Where(m => m.Major.MajorId == id)
                                                .ToListAsync();
            return View(AdvisorDetails);
        }
        public async Task<IActionResult> AvailabilityDetails(int id)
        {
            var availabilityDetails = await _db.Availability.Include(m => m.Advisor)
                                                .Include(m => m.Location)
                                                .Where(m => m.AdvisorId == id)
                                                //.Where(m => m.StartTime.Date == date)
                                                .FirstOrDefaultAsync();
            return View(availabilityDetails);
        }
        public async Task<IActionResult> AvailabilityDetails2(DateTime Date1, int id)
        {

            var availabilityDetails2 = await _db.Availability.Include(m => m.Advisor)
                                                .Include(m => m.Location)
                                                .Where(m => m.StartTime.Date == Date1)
                                                .Where(m=> m.AdvisorId == id)
                                                //.Where(m => m.StartTime.Date == date)
                                                .ToListAsync();
            return View(availabilityDetails2);
        }
        public async Task<IActionResult> AppointmentDetails(int id1, int id2, DateTime time1) {


            AppointmentVM.Appointment.AdvisorId.Equals(id1);
            AppointmentVM.Appointment.LocationId = id2;
            AppointmentVM.Appointment.AppointmentDateTime = time1;
            return View(AppointmentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppointmentDetailsPOST() ///we have data binds 
        {
            if (!ModelState.IsValid)
            {
                return View(AppointmentVM);
            }
            _db.Student.Add(AppointmentVM.Student);
            
            await _db.SaveChangesAsync();
            var studentFromdb = _db.Student.Find(AppointmentVM.Student.StudentId);
            AppointmentVM.Appointment.StudentId = studentFromdb.StudentId;
            _db.Appointment.Add(AppointmentVM.Appointment);
            await _db.SaveChangesAsync();
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

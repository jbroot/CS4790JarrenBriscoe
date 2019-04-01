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
using Microsoft.AspNetCore.Http;

namespace EastAdvising.Controllers
{
    [Area("Student")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnvironment;
        [BindProperty]
        public AppointmentViewModels AppointmentVM { get; set; }
        public const string SessionKeyAdvisor = "_Advisor";
        public string SessionInfo_Advisor { get; private set; }
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
                Availability = _db.Availability.ToList(),
                Appointment = new Models.Appointment()
            };

        }
        public async Task<IActionResult> Index()
        {
            var collegeList = await _db.College.Include(m => m.Departments)
                                                .ToListAsync();

            return View(collegeList);
        }

        public async Task<IActionResult> Cancel()
        {
            

            return View();
        }

        public async Task<IActionResult> Cancel2(string FN, string LN, string EM, DateTime timeDate)
        {
            var DeleteApp = await _db.Appointment.Include(m => m.Student)
                                                .Where(m => m.Student.FirstName == FN)
                                                .Where(m => m.Student.LastName == LN)
                                                .Where(m => m.Student.EmailAddress == EM)
                                                .Where(m => m.AppointmentDateTime == timeDate)
                                                .ToListAsync();
            return View(DeleteApp);
        }

        [HttpPost]
        public async Task<IActionResult> Cancel3(int idA) ///we have data binds 
        {
            /*if (!ModelState.IsValid)
             {
                 return View(AppointmentVM);
             }*/
            var AppointmentCancel = await _db.Appointment.FindAsync(idA);
            AppointmentCancel.Status = 'C';
            _db.Update(AppointmentCancel);
            await _db.SaveChangesAsync();
            return View();

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




        public async Task<IActionResult> AvailabilityDetails2(DateTime Date1, int id, string fullname)
        {
            
            var availabilityDetails2 = await _db.Availability.Include(m => m.Advisor)
                                                .Include(m => m.Location)
                                                .Where(m => m.StartTime.Date == Date1)
                                                .Where(m=> m.AdvisorId == id)
                                                //.Where(m => m.StartTime.Date == date)
                                                .ToListAsync();
            ViewBag.nameA = fullname;
            ViewBag.idA = id;
            ViewBag.date = Date1;
            return View(availabilityDetails2);
        }
        public IActionResult AppointmentDetails(int id1, int id2, int id3, DateTime time1, string campus, string fullname){//int id3) {
                                                                                  //AppointmentVM.Availability = await _db.Availability.Include(m => m.Advisor)
                                                                                  //.Include(m=> m.Services)
                                                                                  //int advId = HttpContext.Session.Get<int>("sadvId");                                                               //.SingleOrDefaultAsync(m => m.AvailabilityId == id3);
                                                                                  //advId = id1;

            //HttpContext.Session.SetInt32(SessionKeyAdvisor, id1);
            ViewBag.adId = id1;
            ViewBag.LocId = id2;
            ViewBag.timeTime = time1;
            ViewBag.AvId = id3;
            ViewBag.AdName = fullname;
            ViewBag.CamN = campus;
            //HttpContext.Session.Set("advId", advId);
            //AppointmentVM.Appointment.LocationId = id2;
            //AppointmentVM.Appointment.AdvisorId = id1;

            //AppointmentVM.Appointment.AppointmentDateTime = time1;
            return View(AppointmentVM);
         }

        

      [HttpPost]
        public async Task<IActionResult> AppointmentDetails2(int AvailId, string AdvName) ///we have data binds 
        {
            /*if (!ModelState.IsValid)
             {
                 return View(AppointmentVM);
             }*/
            ViewBag.newId = AvailId;
            _db.Student.Add(AppointmentVM.Student);
            
            await _db.SaveChangesAsync();
            var studentFromdb = _db.Student.Find(AppointmentVM.Student.StudentId);
            ViewBag.FN = studentFromdb.FirstName;
            ViewBag.LN = studentFromdb.LastName;
            AppointmentVM.Appointment.StudentId = studentFromdb.StudentId;
            // int advId = HttpContext.Session.Get<int>("sadvId");
           
            _db.Appointment.Add(AppointmentVM.Appointment);
            await _db.SaveChangesAsync();
            var AppointmentFromdb = _db.Appointment.Find(AppointmentVM.Appointment.AppointmentId);
            ViewBag.adv = AdvName;
            ViewBag.dateAp = AppointmentFromdb.AppointmentDateTime;
            Availability Av = await _db.Availability.FindAsync(AvailId);
            _db.Availability.Remove(Av);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EastAdvising.Data;
using EastAdvising.Models;

namespace EastAdvising.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Appointments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Appointment.Include(a => a.Advisor).Include(a => a.Location).Include(a => a.Service).Include(a => a.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Advisor)
                .Include(a => a.Location)
                .Include(a => a.Service)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Admin/Appointments/Create
        public IActionResult Create()
        {
            ViewData["AdvisorId"] = new SelectList(_context.Advisor, "AdvisorId", "EmailAddress");
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId");
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId");
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "EmailAddress");
            return View();
        }

        // POST: Admin/Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,StudentId,AdvisorId,LocationId,ServiceId,Comments,AppointmentDateTime,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdvisorId"] = new SelectList(_context.Advisor, "AdvisorId", "EmailAddress", appointment.AdvisorId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", appointment.LocationId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", appointment.ServiceId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "EmailAddress", appointment.StudentId);
            return View(appointment);
        }

        // GET: Admin/Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["AdvisorId"] = new SelectList(_context.Advisor, "AdvisorId", "EmailAddress", appointment.AdvisorId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", appointment.LocationId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", appointment.ServiceId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "EmailAddress", appointment.StudentId);
            return View(appointment);
        }

        // POST: Admin/Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,StudentId,AdvisorId,LocationId,ServiceId,Comments,AppointmentDateTime,Status")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["AdvisorId"] = new SelectList(_context.Advisor, "AdvisorId", "EmailAddress", appointment.AdvisorId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", appointment.LocationId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", appointment.ServiceId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "EmailAddress", appointment.StudentId);
            return View(appointment);
        }

        // GET: Admin/Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Advisor)
                .Include(a => a.Location)
                .Include(a => a.Service)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Admin/Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.AppointmentId == id);
        }
    }
}

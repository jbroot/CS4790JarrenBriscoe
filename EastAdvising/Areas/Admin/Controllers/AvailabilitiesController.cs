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
    public class AvailabilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvailabilitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Availabilities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Availability.Include(a => a.Advisor).Include(a => a.Location);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Availabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availability = await _context.Availability
                .Include(a => a.Advisor)
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.AvailabilityId == id);
            if (availability == null)
            {
                return NotFound();
            }

            return View(availability);
        }

        // GET: Admin/Availabilities/Create
        public IActionResult Create()
        {
            ViewData["AdvisorId"] = new SelectList(_context.Advisor, "AdvisorId", "EmailAddress");
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId");
            return View();
        }

        // POST: Admin/Availabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvailabilityId,AdvisorId,LocationId,StartTime,EndTime")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdvisorId"] = new SelectList(_context.Advisor, "AdvisorId", "EmailAddress", availability.AdvisorId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", availability.LocationId);
            return View(availability);
        }

        // GET: Admin/Availabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availability = await _context.Availability.FindAsync(id);
            if (availability == null)
            {
                return NotFound();
            }
            ViewData["AdvisorId"] = new SelectList(_context.Advisor, "AdvisorId", "EmailAddress", availability.AdvisorId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", availability.LocationId);
            return View(availability);
        }

        // POST: Admin/Availabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvailabilityId,AdvisorId,LocationId,StartTime,EndTime")] Availability availability)
        {
            if (id != availability.AvailabilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailabilityExists(availability.AvailabilityId))
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
            ViewData["AdvisorId"] = new SelectList(_context.Advisor, "AdvisorId", "EmailAddress", availability.AdvisorId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", availability.LocationId);
            return View(availability);
        }

        // GET: Admin/Availabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availability = await _context.Availability
                .Include(a => a.Advisor)
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.AvailabilityId == id);
            if (availability == null)
            {
                return NotFound();
            }

            return View(availability);
        }

        // POST: Admin/Availabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availability = await _context.Availability.FindAsync(id);
            _context.Availability.Remove(availability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailabilityExists(int id)
        {
            return _context.Availability.Any(e => e.AvailabilityId == id);
        }
    }
}

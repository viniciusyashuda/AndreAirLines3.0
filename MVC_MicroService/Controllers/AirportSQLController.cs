using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_MicroService.Data;
using Model;

namespace MVC_MicroService.Controllers
{
    public class AirportSQLController : Controller
    {
        private readonly MVC_MicroServiceContext _context;

        public AirportSQLController(MVC_MicroServiceContext context)
        {
            _context = context;
        }

        // GET: AirportSQL
        public async Task<IActionResult> Index()
        {
            return View(await _context.AirportSQL.ToListAsync());
        }

        // GET: AirportSQL/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportSQL = await _context.AirportSQL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airportSQL == null)
            {
                return NotFound();
            }

            return View(airportSQL);
        }

        // GET: AirportSQL/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AirportSQL/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,Country,Code,Continent")] AirportSQL airportSQL)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airportSQL);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(airportSQL);
        }

        // GET: AirportSQL/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportSQL = await _context.AirportSQL.FindAsync(id);
            if (airportSQL == null)
            {
                return NotFound();
            }
            return View(airportSQL);
        }

        // POST: AirportSQL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,Country,Code,Continent")] AirportSQL airportSQL)
        {
            if (id != airportSQL.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airportSQL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportSQLExists(airportSQL.Id))
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
            return View(airportSQL);
        }

        // GET: AirportSQL/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportSQL = await _context.AirportSQL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airportSQL == null)
            {
                return NotFound();
            }

            return View(airportSQL);
        }

        // POST: AirportSQL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airportSQL = await _context.AirportSQL.FindAsync(id);
            _context.AirportSQL.Remove(airportSQL);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirportSQLExists(int id)
        {
            return _context.AirportSQL.Any(e => e.Id == id);
        }
    }
}

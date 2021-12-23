using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmployeeDtlsController : Controller
    {
        private readonly EmployeeDtlsContext _context;

        public EmployeeDtlsController(EmployeeDtlsContext context)
        {
            _context = context;
        }

        // GET: EmployeeDtls
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeDtl.ToListAsync());
        }

        // GET: EmployeeDtls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDtl = await _context.EmployeeDtl
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeDtl == null)
            {
                return NotFound();
            }

            return View(employeeDtl);
        }

        // GET: EmployeeDtls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeDtls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,Address,Designation,Salary,JoiningDate")] EmployeeDtl employeeDtl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeDtl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDtl);
        }

        // GET: EmployeeDtls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDtl = await _context.EmployeeDtl.FindAsync(id);
            if (employeeDtl == null)
            {
                return NotFound();
            }
            return View(employeeDtl);
        }

        // POST: EmployeeDtls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,Address,Designation,Salary,JoiningDate")] EmployeeDtl employeeDtl)
        {
            if (id != employeeDtl.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeDtl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDtlExists(employeeDtl.EmployeeId))
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
            return View(employeeDtl);
        }

        // GET: EmployeeDtls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDtl = await _context.EmployeeDtl
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeDtl == null)
            {
                return NotFound();
            }

            return View(employeeDtl);
        }

        // POST: EmployeeDtls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeDtl = await _context.EmployeeDtl.FindAsync(id);
            _context.EmployeeDtl.Remove(employeeDtl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDtlExists(int id)
        {
            return _context.EmployeeDtl.Any(e => e.EmployeeId == id);
        }
    }
}

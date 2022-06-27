using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibraryASP.Models;
using MyLibraryASP.Data;

namespace MyLibraryASP.Controllers
{
    public class LablesController : Controller
    {
        private readonly MyLibraryASPContext _context;

        public LablesController(MyLibraryASPContext context)
        {
            _context = context;
        }

        // GET: Lables
        public async Task<IActionResult> Index()
        {
              return _context.Lable != null ? 
                          View(await _context.Lable.ToListAsync()) :
                          Problem("Entity set 'MyLibraryASPContext.Lable'  is null.");
        }

        // GET: Lables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lable == null)
            {
                return NotFound();
            }

            var lable = await _context.Lable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lable == null)
            {
                return NotFound();
            }

            return View(lable);
        }

        // GET: Lables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Lable lable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lable);
        }

        // GET: Lables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lable == null)
            {
                return NotFound();
            }

            var lable = await _context.Lable.FindAsync(id);
            if (lable == null)
            {
                return NotFound();
            }
            return View(lable);
        }

        // POST: Lables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Lable lable)
        {
            if (id != lable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LableExists(lable.Id))
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
            return View(lable);
        }

        // GET: Lables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lable == null)
            {
                return NotFound();
            }

            var lable = await _context.Lable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lable == null)
            {
                return NotFound();
            }

            return View(lable);
        }

        // POST: Lables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lable == null)
            {
                return Problem("Entity set 'MyLibraryASPContext.Lable'  is null.");
            }
            var lable = await _context.Lable.FindAsync(id);
            if (lable != null)
            {
                _context.Lable.Remove(lable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LableExists(int id)
        {
          return (_context.Lable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

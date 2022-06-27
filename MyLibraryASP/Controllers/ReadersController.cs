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
    public class ReadersController : Controller
    {
        private readonly MyLibraryASPContext _context;

        public ReadersController(MyLibraryASPContext context)
        {
            _context = context;
        }

        // GET: Readers
        public async Task<IActionResult> Index()
        {
              return _context.Reader != null ? 
                          View(await _context.Reader.ToListAsync()) :
                          Problem("Entity set 'MyLibraryASPContext.Reader'  is null.");
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reader == null)
            {
                return NotFound();
            }

            var reader = await _context.Reader
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReleaseDate,BirthDate,Name,Lastname,MiddleName")] Reader reader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reader == null)
            {
                return NotFound();
            }

            var reader = await _context.Reader.FindAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReleaseDate,BirthDate,Name,Lastname,MiddleName")] Reader reader)
        {
            if (id != reader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaderExists(reader.Id))
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
            return View(reader);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reader == null)
            {
                return NotFound();
            }

            var reader = await _context.Reader
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reader == null)
            {
                return Problem("Entity set 'MyLibraryASPContext.Reader'  is null.");
            }
            var reader = await _context.Reader.FindAsync(id);
            if (reader != null)
            {
                _context.Reader.Remove(reader);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaderExists(int id)
        {
          return (_context.Reader?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

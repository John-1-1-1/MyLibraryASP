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
    public class ReadingLogsController : Controller
    {
        private readonly MyLibraryASPContext _context;

        public ReadingLogsController(MyLibraryASPContext context)
        {
            _context = context;
        }

        // GET: ReadingLogs
        public async Task<IActionResult> Index()
        {
            var myLibraryASPContext = _context.ReadingLog.Include(r => r.Book).Include(r => r.Reader);
            return View(await myLibraryASPContext.ToListAsync());
        }

        // GET: ReadingLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReadingLog == null)
            {
                return NotFound();
            }

            var readingLog = await _context.ReadingLog
                .Include(r => r.Book)
                .Include(r => r.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readingLog == null)
            {
                return NotFound();
            }

            return View(readingLog);
        }

        // GET: ReadingLogs/Create
        public IActionResult Create()
        {
            var readers = from d in _context.Autor
                            select new
                            {
                                Id = d.Id,
                                Name = d.Lastname + " " + d.Name + " " + d.MiddleName
                            };

            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name");
            ViewData["ReaderId"] = new SelectList(readers, "Id", "Name");
            return View();
        }

        // POST: ReadingLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,ReaderId,DateOfIssue,DateOfReturn")] ReadingLog readingLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(readingLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var readers = from d in _context.Autor
                          select new
                          {
                              Id = d.Id,
                              Name = d.Lastname + " " + d.Name + " " + d.MiddleName
                          };
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", readingLog.BookId);
            ViewData["ReaderId"] = new SelectList(readers, "Id", "Name", readingLog.ReaderId);
            return View(readingLog);
        }

        // GET: ReadingLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReadingLog == null)
            {
                return NotFound();
            }

            var readingLog = await _context.ReadingLog.FindAsync(id);
            if (readingLog == null)
            {
                return NotFound();
            }

            var readers = from d in _context.Autor
                          select new
                          {
                              Id = d.Id,
                              Name = d.Lastname + " " + d.Name + " " + d.MiddleName
                          };
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", readingLog.BookId);
            ViewData["ReaderId"] = new SelectList(readers, "Id", "Name", readingLog.ReaderId);
            return View(readingLog);
        }

        // POST: ReadingLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,ReaderId,DateOfIssue,DateOfReturn")] ReadingLog readingLog)
        {
            if (id != readingLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(readingLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadingLogExists(readingLog.Id))
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

            var readers = from d in _context.Autor
                          select new
                          {
                              Id = d.Id,
                              Name = d.Lastname + " " + d.Name + " " + d.MiddleName
                          };
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Name", readingLog.BookId);
            ViewData["ReaderId"] = new SelectList(readers, "Id", "Name", readingLog.ReaderId);
            return View(readingLog);
        }

        // GET: ReadingLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReadingLog == null)
            {
                return NotFound();
            }

            var readingLog = await _context.ReadingLog
                .Include(r => r.Book)
                .Include(r => r.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readingLog == null)
            {
                return NotFound();
            }

            return View(readingLog);
        }

        // POST: ReadingLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReadingLog == null)
            {
                return Problem("Entity set 'MyLibraryASPContext.ReadingLog'  is null.");
            }
            var readingLog = await _context.ReadingLog.FindAsync(id);
            if (readingLog != null)
            {
                _context.ReadingLog.Remove(readingLog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReadingLogExists(int id)
        {
          return (_context.ReadingLog?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

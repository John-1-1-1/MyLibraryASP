using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibraryASP.Models;
using MyLibraryASP.Models.Additional;
using MyLibraryASP.Data;

namespace MyLibraryASP.Controllers
{
    public class BooksController : Controller
    {
        private readonly MyLibraryASPContext _context;

        public BooksController(MyLibraryASPContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var myLibraryASPContext = _context.Book.Include(b => b.Autor).Include(b => b.Category).Include(b => b.Lable).Include(b => b.Shelf);
            return View(await myLibraryASPContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Autor)
                .Include(b => b.Category)
                .Include(b => b.Lable)
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {

            var countries = from d in _context.Autor
                            select new
                            {
                                Id = d.Id,
                                Name = d.Lastname + " " + d.Name + " " + d.MiddleName
                            };

            ViewData["AutorId"] = new SelectList(countries, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            ViewData["LableId"] = new SelectList(_context.Set<Lable>(), "Id", "Name");
            ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookAdditionalModel bookAdditionalModel)
        {
            Book book = new Book
            {
                Id = bookAdditionalModel.Id,
                Name = bookAdditionalModel.Name,
                AutorId = bookAdditionalModel.AutorId,
                CategoryId = bookAdditionalModel.CategoryId,
                Autor = bookAdditionalModel.Autor,
                Shelf = bookAdditionalModel.Shelf,
                Category = bookAdditionalModel.Category,
                ShelfId = bookAdditionalModel.ShelfId,
                LableId = bookAdditionalModel.LableId,
                Lable = bookAdditionalModel.Lable,
            };

            if (ModelState.IsValid)
            {
                if (bookAdditionalModel.Image != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(
                        bookAdditionalModel.Image
                        .OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes(
                            (int)bookAdditionalModel.Image.Length);
                    }
                    // установка массива байтов
                    book.Image = imageData;

                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            var countries = from d in _context.Autor
                            select new
                            {
                                Id = d.Id,
                                Name = d.Lastname + " " + d.Name + " " + d.MiddleName
                            };


            ViewData["AutorId"] = new SelectList(countries, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", book.CategoryId);
            ViewData["LableId"] = new SelectList(_context.Set<Lable>(), "Id", "Name", book.LableId);
            ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Name", book.ShelfId);
            return View(bookAdditionalModel);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var countries = from d in _context.Autor
                            select new
                            {
                                Id = d.Id,
                                Name = d.Lastname + " " + d.Name + " " + d.MiddleName
                            };

            ViewData["AutorId"] = new SelectList(countries, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", book.CategoryId);
            ViewData["LableId"] = new SelectList(_context.Set<Lable>(), "Id", "Name", book.LableId);
            ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Name", book.ShelfId);
            
            BookAdditionalModel bookAdditionalModel = new BookAdditionalModel
             {
                 Id = book.Id,
                 Name = book.Name,
                 AutorId = book.AutorId,
                 CategoryId = book.CategoryId,
                 ShelfId = book.ShelfId,
                 LableId = book.LableId
             };

            return View(bookAdditionalModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookAdditionalModel bookAdditionalModel)
        {
            Book book = new Book
            {
                Id = bookAdditionalModel.Id,
                Name = bookAdditionalModel.Name,
                AutorId = bookAdditionalModel.AutorId,
                CategoryId = bookAdditionalModel.CategoryId,
                ShelfId = bookAdditionalModel.ShelfId,
                LableId = bookAdditionalModel.LableId,
            };

            if (bookAdditionalModel.Image == null)
            {
                var Image = _context.Book?.
                    AsNoTracking().FirstOrDefault(a => a.Id == id);
                book.Image = Image!.Image;
            }
            else
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(
                    bookAdditionalModel.Image
                    .OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes(
                        (int)bookAdditionalModel.Image.Length);
                }
                // установка массива байтов
                book.Image = imageData;
            }

            if (id != book.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            if (book.Name != null || book.Name != "")
                return RedirectToAction(nameof(Index));
            var countries = from d in _context.Autor
                            select new
                            {
                                Id = d.Id,
                                Name = d.Lastname + " " + d.Name + " " + d.MiddleName
                            };

            ViewData["AutorName"] = new SelectList(countries, "Id", "Name");
            ViewData["CategoryName"] = new SelectList(_context.Set<Category>(), "Id", "Name", book.CategoryId);
            ViewData["LableName"] = new SelectList(_context.Set<Lable>(), "Id", "Name", book.LableId);
            ViewData["ShelfName"] = new SelectList(_context.Set<Shelf>(), "Id", "Name", book.ShelfId);

            return Redirect("/Books/Index");
            //if (id != book.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(book);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!BookExists(book.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["AutorId"] = new SelectList(_context.Autor, "Id", "Id", book.AutorId);
            //ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Id", book.CategoryId);
            //ViewData["LableId"] = new SelectList(_context.Set<Lable>(), "Id", "Id", book.LableId);
            //ViewData["ShelfId"] = new SelectList(_context.Set<Shelf>(), "Id", "Id", book.ShelfId);
            //return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Autor)
                .Include(b => b.Category)
                .Include(b => b.Lable)
                .Include(b => b.Shelf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MyLibraryASPContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

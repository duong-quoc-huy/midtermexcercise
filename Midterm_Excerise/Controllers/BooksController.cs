using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Midterm_Excerise.Data;
using Midterm_Excerise.Models;

namespace Midterm_Excerise.Controllers
{
    public class BooksController : Controller
    {
        private readonly MvcNiieBookManagementContext _context;

        public BooksController(MvcNiieBookManagementContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string? TuKhoa)
        {
            var data = _context.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrEmpty(TuKhoa))
            {
                data = data.Where(s =>
                    s.BookId.ToString().Contains(TuKhoa) ||
                    s.Title.Contains(TuKhoa) ||
                    s.Description.Contains(TuKhoa) ||
                    s.CoverImagePath.Contains(TuKhoa)
                );
            }

            return View(await data.ToListAsync());
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Description,CoverImagePath,AuthorId")] Book book, IFormFile FileLogo)
        {
            if (FileLogo != null)
            {
                var path = MyTool.UploadImageToFolder(FileLogo, "covers");
                if (string.IsNullOrEmpty(path))
                {
                    ModelState.AddModelError("CoverImagePath", "Invalid file. Only .jpg, .jpeg, .png allowed. Max 2MB.");
                }
                else
                {
                    book.CoverImagePath = path;
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", book.AuthorId);
            return View(book);
        }

        

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Description,CoverImagePath,AuthorId")] Book book, IFormFile FileLogo)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (FileLogo != null)
            {
                var path = MyTool.UploadImageToFolder(FileLogo, "covers");
                if (!string.IsNullOrEmpty(path))
                {
                    book.CoverImagePath = path;
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
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
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }


    }
}

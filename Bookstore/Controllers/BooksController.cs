using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }
        // GET: Books
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var books = await _context.Book
             .Where(ti => ti.ApplicationUserId == user.Id)
             .Include(ti => ti.BookGenres)
                .ThenInclude(bg => bg.Genre)
             .ToListAsync();

            return View(books);

          
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: Books/Create
        public async Task<ActionResult> Create()
        {
            var GenreOptions = await _context.Genre
              .Select(g => new SelectListItem() { Text = g.Name, Value = g.Id.ToString() })
              .ToListAsync();

            var viewModel = new BookFormViewModel();

            viewModel.GenreOptions = GenreOptions;

            return View(viewModel);

        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookFormViewModel bookViewModel)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var bookDataModel = new Book
                {
                    Title = bookViewModel.Title,
                    Author = bookViewModel.Author,
                    ApplicationUserId = user.Id
                };

                bookDataModel.BookGenres = bookViewModel.SelectedGenreIds.Select(gi =>

                    new BookGenre()
                    {
                        BookId = bookDataModel.Id,
                        GenreId = gi
                    }).ToList();

                _context.Book.Add(bookDataModel);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _context.Book.Include(b => b.BookGenres).FirstOrDefaultAsync(ti => ti.Id == id);

            var loggedInUser = await GetCurrentUserAsync();

            var viewModel = new BookFormViewModel(); 

            var GenreOptions = await _context.Genre
               .Select(g => new SelectListItem() { Text = g.Name, Value = g.Id.ToString() })
               .ToListAsync();


            viewModel.Id = id;
            viewModel.Title = item.Title;
            viewModel.Author = item.Author;
            viewModel.GenreOptions = GenreOptions;
            viewModel.SelectedGenreIds = item.BookGenres.Select(bg => bg.GenreId).ToList();

          

            if (item.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }

            return View(viewModel);
            
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BookFormViewModel bookViewModel)
        {
            try
            {
                var bookDataModel = await _context.Book.Include(b => b.BookGenres).FirstOrDefaultAsync(b => b.Id == id);

                bookDataModel.Title = bookViewModel.Title;
                bookDataModel.Author = bookViewModel.Author;
                bookDataModel.BookGenres.Clear();
                bookDataModel.BookGenres = bookViewModel.SelectedGenreIds.Select(gi =>
                 new BookGenre
                 {
                     BookId = bookDataModel.Id,
                     GenreId = gi

                 }).ToList();

                _context.Book.Update(bookDataModel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _context.Book
                .Include(bg => bg.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(i => i.Id == id);

            return View(item);
          
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Book book)
        {
            try
            {
                 _context.Book.Remove(book);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
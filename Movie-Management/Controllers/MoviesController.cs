using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Models.ViewModels;
using MovieManagement.Data;
using MovieManagement.Models.DataModels;

namespace MovieManagement.Controllers
{
    public class MoviesController : Controller
    {
        /* 
         * NOTE:
         * Dependency Injection: The constructor uses Dependency Injection to inject the database context (MovieManagementDBContext) into the controller.
         * The database context is used in each of the CRUD methods in the controller.
         */
        private readonly MovieManagementDBContext _context;

        public MoviesController(MovieManagementDBContext context)
        {
            _context = context;
        }

        // GET: Movies
        [HttpGet]
        public async Task<IActionResult> Index(string selectedGenre, string searchString)
        {
            // Language-Integrated Query (LINQ): select list of genres from database.
            var genres = from m in _context.Movie
                         orderby m.Genre
                         select m.Genre;

            // Language-Integrated Query (LINQ): select list of movies from database.
            var movies = from m in _context.Movie
                         select m;

            // Filter by movie title.
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Title.Contains(searchString));
            }

            // Filter by movie genre.
            if (!String.IsNullOrEmpty(selectedGenre))
            {
                movies = movies.Where(m => m.Genre == selectedGenre);
            }

            var movieVM = new MovieViewModel
            {
                // NOTE: Asynchronous Programming with async and await - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
                Genres = new SelectList(await genres.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync(),
                SelectedGenre = selectedGenre,
                SearchString = searchString
            };

            return View(movieVM);
        }

        // GET: Movies/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)       // ?: Nullable Type
        {
            if (id == null)
            {
                return NotFound();
            }

            // Select the first movie instance that match the route data or query string value.
            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

            // If a movie is not found, return the result not found.
            if (movie == null)
            {
                return NotFound();
            }

            // If a movie is found, an instance of the Movie model is passed to the Details view.
            return View(movie);
        }

        // GET: Movies/Create
        [HttpGet]
        public IActionResult Create()
        {
            // ViewResult object must implement the IActionResult interface.
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            // If the data state is valid, the data will be created.
            if (ModelState.IsValid)
            {
                _context.Add(movie);

                // The new movie data is saved to the database by calling the SaveChangesAsync method of database context.
                await _context.SaveChangesAsync();

                // Redirect the user to the Index action: displays the movie collection.
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            // The [Bind] attribute is one way to protect against over-posting: You should only include properties in the [Bind] attribute that you want to change.

            if (id != movie.Id)
            {
                return NotFound();
            }

            // If the data state is valid, the data will be updated.
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);

                    // The updated movie data is saved to the database by calling the SaveChangesAsync method of database context.
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Redirect the user to the Index action: displays the movie collection.
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // GET: Movies/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}

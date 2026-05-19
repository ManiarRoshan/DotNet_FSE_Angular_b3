using Microsoft.AspNetCore.Mvc;
using MovieCatlog_repo_service_pattern.Models;
using MovieCatlog_repo_service_pattern.Services;
namespace MovieCatlog_repo_service_pattern.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;
        public MoviesController(IMovieService service) => _service = service;

        public IActionResult Index() => View(_service.GetMovieList());

        public IActionResult Details(int id) => View(_service.GetMovieDetails(id));

        // GET: Create
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _service.CreateMovie(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Edit
        public IActionResult Edit(int id) => View(_service.GetMovieDetails(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateMovie(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Delete
        public IActionResult Delete(int id) => View(_service.GetMovieDetails(id));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.RemoveMovie(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

using EntityFC.Models;
using Microsoft.AspNetCore.Mvc;
using EntityFC.Services;
using System.Linq;

namespace EntityFC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            _service.Add(movie);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            _service.Update(movie);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            _service.Delete(movie.Id);
            return RedirectToAction("Index");
        }
    }
}

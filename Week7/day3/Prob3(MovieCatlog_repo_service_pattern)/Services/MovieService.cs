using MovieCatlog_repo_service_pattern.Models;
using MovieCatlog_repo_service_pattern.Repositories;

namespace MovieCatlog_repo_service_pattern.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        public MovieService(IMovieRepository repository) => _repository = repository;

        public IEnumerable<Movie> GetMovieList() => _repository.GetAll();
        public Movie GetMovieDetails(int id) => _repository.GetById(id);

        public void CreateMovie(Movie movie)
        {
            _repository.Add(movie);
            _repository.Save();
        }

        public void UpdateMovie(Movie movie)
        {
            _repository.Update(movie);
            _repository.Save();
        }

        public void RemoveMovie(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }
    }
}

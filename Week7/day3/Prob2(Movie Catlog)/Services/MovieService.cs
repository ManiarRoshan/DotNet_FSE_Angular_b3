using EntityFC.Models;
using EntityFC.Repositories;
namespace EntityFC.Services
{
    public class MovieService: IMovieService
    {
        private readonly IMovieRepo _repository;
        public MovieService(IMovieRepo repository)
        {
            _repository = repository;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _repository.GetAll();
        }

        public Movie GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Add(Movie movie)
        {
            _repository.Add(movie);
        }

        public void Update(Movie movie)
        {
            _repository.Update(movie);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}

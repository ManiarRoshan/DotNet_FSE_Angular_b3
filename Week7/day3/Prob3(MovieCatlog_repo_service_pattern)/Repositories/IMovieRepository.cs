using MovieCatlog_repo_service_pattern.Models;
namespace MovieCatlog_repo_service_pattern.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie GetById(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
        void Save();
    }
}

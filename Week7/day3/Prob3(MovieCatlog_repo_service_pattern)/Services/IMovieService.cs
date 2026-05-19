using MovieCatlog_repo_service_pattern.Models;

namespace MovieCatlog_repo_service_pattern.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetMovieList();
        Movie GetMovieDetails(int id);
        void CreateMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void RemoveMovie(int id);
    }
}

using MovieCatlog_repo_service_pattern.Models;

namespace MovieCatlog_repo_service_pattern.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Movie> GetAll() => _context.Movies.ToList();
        public Movie GetById(int id) => _context.Movies.Find(id);
        public void Add(Movie movie) => _context.Movies.Add(movie);
        public void Update(Movie movie)
        {
            // Fetch the existing record from the DB so EF starts tracking it
            var existing = _context.Movies.Find(movie.Id);
            if (existing != null)
            {
                // Update only the properties you want to change
                _context.Entry(existing).CurrentValues.SetValues(movie);
            }
        }

        public void Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null) _context.Movies.Remove(movie);
        }
        public void Save() => _context.SaveChanges();
    }
}

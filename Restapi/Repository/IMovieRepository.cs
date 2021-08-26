using Restapi.Domain;
using Restapi.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Repository
{
    public interface IMovieRepository
    {
        public Task<bool> SaveMovieAsync(MovieRequestModel movie);
        public Task<Movie> GetMovieByNameAsync(string name);
        public Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
        public Task<IEnumerable<Movie>> GetAllMoviesAsync();
    }
}

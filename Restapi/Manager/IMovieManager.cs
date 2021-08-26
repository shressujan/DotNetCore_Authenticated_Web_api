using Restapi.enums;
using Restapi.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Manager
{
    public interface IMovieManager
    {
        public Task<bool> AddMovies(MovieRequestModel movie);
        public Task<MovieModel> GetMovieByName(string name);
        public Task<IEnumerable<MovieModel>> GetMovieByGenre(string genre);
        public Task<IEnumerable<MovieModel>> GetAllMovies();
    }
}

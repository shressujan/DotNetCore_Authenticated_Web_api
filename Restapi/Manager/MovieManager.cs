using Restapi.enums;
using Restapi.Models.Request;
using Restapi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Manager
{
    public class MovieManager : IMovieManager
    {
        private readonly IMovieRepository _movieRepository;

        public MovieManager(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieModel>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();
            return movies.Select(c => new MovieModel(c));
        }

        public async Task<bool> AddMovies(MovieRequestModel movie)
        {
            var result = await _movieRepository.SaveMovieAsync(movie);
            return result;
        }

        public async Task<IEnumerable<MovieModel>> GetMovieByGenre(string genre)
        {
            var movies = await _movieRepository.GetMoviesByGenreAsync(genre);
            return movies.Select(c => new MovieModel(c));
        }

        public async Task<MovieModel> GetMovieByName(string name)
        {
            var movie = await _movieRepository.GetMovieByNameAsync(name);
            return new MovieModel(movie);
        }
    }
}

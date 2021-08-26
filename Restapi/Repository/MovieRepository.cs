using Dapper;
using Restapi.Domain;
using Restapi.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public MovieRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Movie> GetMovieByNameAsync(string name)
        {
            var param = new DynamicParameters();
            param.Add("@name", name);

            var query = "SELECT * FROM movie WHERE movieName=@name";
            using (var connection = _unitOfWork.CreateConnection())
            {
                var movie = await connection.QuerySingleOrDefaultAsync<Movie>(query, param);
                return movie;
            }
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
        {
            var param = new DynamicParameters();
            param.Add("@genre", genre);

            var query = "SELECT * FROM movie WHERE genre=@genre";
            using (var connection = _unitOfWork.CreateConnection())
            {
                var movies = await connection.QueryAsync<Movie>(query, param);
                return movies;
            }
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            var query = "SELECT * FROM netflix.movie";
            using (var connection = _unitOfWork.CreateConnection())
            {
                var movies = await connection.QueryAsync<Movie>(query);
                return movies;
            }
        }

        public async Task<bool> SaveMovieAsync(MovieRequestModel movie)
        {
            var param = new DynamicParameters();
            param.Add("@movieName", movie.Moviename);
            param.Add("@genre", movie.Genre.ToString());
            param.Add("@rating", movie.Rating);

            var query = @"INSERT INTO movie(movieName, genre, rating) VALUES (@movieName, @genre, @rating)";

            using (var connection = _unitOfWork.CreateConnection())
            {
                await connection.ExecuteAsync(query, param);
                return true;
            }
        }
    }
}

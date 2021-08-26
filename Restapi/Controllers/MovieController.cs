using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restapi.enums;
using Restapi.Manager;
using Restapi.Models;
using Restapi.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMovieManager _movieManager;
        public MovieController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        [HttpGet("Name")]
        public async Task<IActionResult> Name([FromQuery] string name)
        {
            var movie = await _movieManager.GetMovieByName(name);
            return Ok(movie);
        }

        [HttpGet("Genre")]
        public async Task<IActionResult> Genre([FromQuery] string genre)
        {
            var movies = await _movieManager.GetMovieByGenre(genre);
            return Ok(movies);
        }

        [HttpGet("Movies")]
        public async Task<IActionResult> Movies()
        {
            var movies = await _movieManager.GetAllMovies();
            return Ok(movies);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] MovieRequestModel movie)
        {
            var result = await _movieManager.AddMovies(movie);
            return Ok(result);
        }

    }
}

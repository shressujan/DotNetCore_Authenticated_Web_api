using Restapi.Domain;
using Restapi.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Models.Request
{
    public class MovieModel
    {
        private readonly Movie _entity;
        public MovieModel(Movie entity)
        {
            _entity = entity;
        }
        [Required]
        public string Moviename => _entity.Moviename;
        [Required]
        public Genres Genre => _entity.Genre;
        [Required]
        public float Rating => _entity.Rating;
    }
}

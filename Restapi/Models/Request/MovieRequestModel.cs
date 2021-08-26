using Restapi.Domain;
using Restapi.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Models.Request
{
    public class MovieRequestModel
    {
        [Required]
        public string Moviename { get; set; }
        [Required]
        public Genres Genre { get; set; }
        [Required]
        public float Rating { get; set; }
    }
}

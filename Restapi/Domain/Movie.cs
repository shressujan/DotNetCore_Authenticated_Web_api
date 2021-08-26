using Restapi.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restapi.Domain
{
    public class Movie
    {

        public Movie() {}
        public string Moviename { get; set; }
        public Genres Genre { get; set; }
        public float Rating { get; set; }
    }
}

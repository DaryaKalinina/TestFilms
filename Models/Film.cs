using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFilms.Models
{
    public class Film
    {
        public Film()
        {
            UsersFavoriteFilms = new List<UserFavoriteFilm>();
            Reviews = new List<Review>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Actor { get; set; }
        public ICollection<UserFavoriteFilm> UsersFavoriteFilms { get; set; }
        public ICollection<Review> Reviews { get; set; }


    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFilms.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            UsersFavoriteFilms = new List<UserFavoriteFilm>();
            Reviews = new List<Review>();
        }
        public ICollection<UserFavoriteFilm> UsersFavoriteFilms { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}

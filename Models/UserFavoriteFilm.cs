using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFilms.Models
{
    public class UserFavoriteFilm
    {
        //public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int FilmId { get; set; }
        public virtual Film Film { get; set; }
    }
}

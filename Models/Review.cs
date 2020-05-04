using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestFilms.Models
{
    public class Review
    {
        public Review()
        {

        }
       // public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int FilmId { get; set; }
        public virtual Film Film { get; set; }             
        public string Text { get; set; }

    }
}

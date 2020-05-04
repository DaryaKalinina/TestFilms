using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestFilms.Data;
using TestFilms.Models;

namespace TestFilms.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;

        }
        /// <summary>
        /// получение списка любимых фильмов пользователя.
        /// </summary>
        /// <returns></returns>
        [Route("/user")]
        [HttpGet]
        public IActionResult Get()
        {
            var usrName = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.UserName == usrName);
            var listOfFilms = _db.UsersFavoriteFilms.Select(x => x.UserId == user.Id);
            if (listOfFilms == null)
            {
                return NotFound();
            }
            return Ok(listOfFilms);

        }
    }
}
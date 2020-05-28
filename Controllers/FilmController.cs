using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestFilms.Data;
using TestFilms.Models;

namespace TestFilms.Controllers
{
    public class FilmController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public FilmController(ApplicationDbContext db) 
        {
            _db = db;
        }
        /// <summary>
        /// Получение всех фильмов
        /// </summary>
        /// <returns></returns>
        [Route("/films")]
        [HttpGet]
        public IActionResult GetAllFilms()
        {
            var listOfFilms =  _db.Films;
            if (listOfFilms == null)
            {
                return NotFound();
            }
            return Ok(listOfFilms);
           
        }
        /// <summary>
        /// Добавление нового фильма
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("/film")]
        [HttpPost]
        public async Task<IActionResult> AddNewFilm(Film model)
        {
            _db.Films.Add(model);
            await _db.SaveChangesAsync();
            return Ok(model);
        }
        /// <summary>
        /// Добавление фильма в список понравившихся 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("/films")]
        [HttpPost]
        public IActionResult AddToFavorite(Film model)
        {
            var usrName = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.UserName == usrName);
            try
            {
                var isFilmFavoriteForUser = _db.UsersFavoriteFilms.Any(u => u.UserId == user.Id && u.FilmId == model.Id);

                if (isFilmFavoriteForUser == true)
                {
                    return Ok("You have already liked this movie!");

                }

                Film currfilm = _db.Films.FirstOrDefault(x => x.Id == model.Id);
                UserFavoriteFilm favoriteFilm = new UserFavoriteFilm();
                favoriteFilm.UserId = user.Id;

                favoriteFilm.FilmId = currfilm.Id;

                _db.UsersFavoriteFilms.Add(favoriteFilm);


                _db.SaveChanges();
                return Ok("You liked this movie");
            }
            catch (InvalidCastException e)
            {
                return BadRequest(e);
            }

        }
        /// <summary>
        /// Поиск фильма по жанру, актеру и режиссеру
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="actor"></param>
        /// <param name="director"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/searchfilms")]
        public IActionResult SearchFilm(string genre,string actor,string director)
        {
            IQueryable<Film> query = (IQueryable<Film>)_db.Films;
            //все поля заполнены 111
            if(genre != null && actor!=null && director!=null)
            {
                query = query.Where(x => x.Genre == genre && x.Actor == actor && x.Director == director);
                
            }
            //110
            if (genre != null && actor != null && director == null)
            {
                query = query.Where(x => x.Genre == genre && x.Actor == actor);
                
            }
            // 101
            if (genre != null && actor == null && director != null) {

                query = query.Where(x => x.Genre == genre && x.Director == director);
                
            }
            //100
            if (genre != null && actor == null && director == null)
            {
                query = query.Where(x => x.Genre == genre);
                
            }
            //010
            if (genre == null && actor != null && director == null)
            {
                query = query.Where(x => x.Actor == actor);
                
            }
            //011
            if (genre == null && actor != null && director != null)
            {
                query = query.Where(x => x.Actor == actor && x.Director == director);
                
            }
            //001
            if (genre == null && actor == null && director != null)
            {
                query = query.Where(x => x.Director == director);
                
            }
            if (genre == null && actor == null && director == null)
            {
                
                return Ok("You didn't fill in the search parameters");
            }
            var resultSearch = query.ToList();
            if (resultSearch.Count == 0)
            {
                return Ok("I have nothing to show you.");
            }
            return Ok(resultSearch);

            

        }
        /// <summary>
        /// Добавление отзыва к фильму
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("/review")]
        [HttpPost]
        public IActionResult AddReview(int filmId, string text)
        {
           
            var usrName = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.UserName == usrName);
            try
            {
                var isUserCommentedFilm = _db.Reviews.Any(u => u.UserId == user.Id && u.FilmId == filmId);

                if (isUserCommentedFilm == true)

                {
                    return Ok("You have already commented this movie!");

                }

                Review review = new Review();
                review.FilmId = filmId;
                review.UserId = user.Id;
                review.Text = text;
                _db.Reviews.Add(review);
                _db.SaveChanges();
                return Ok("Comment added");
            }
            catch (InvalidCastException e)
            {
                return BadRequest(e);
            }


        }

    }
    

    
}
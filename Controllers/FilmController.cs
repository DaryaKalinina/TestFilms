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
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("/film")]
        [HttpGet]
        public IActionResult Get()
        {
            var listOfFilms =  _db.Films;
            if (listOfFilms == null)
            {
                return NotFound();
            }
            return Ok(listOfFilms);
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("/film")]
        [HttpPost]
        public async Task<IActionResult> Post(Film model)
        {
            _db.Films.Add(model);
            await _db.SaveChangesAsync();
            return Ok(model);
        }
        /// <summary>
        /// добавление фильма в список понравившися
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("/film")]
        [HttpPut]
        public IActionResult Put(Film model)
        {
            var usrName = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.UserName == usrName);
            var check_user = _db.UsersFavoriteFilms.Where(u => u.UserId == user.Id && u.FilmId == model.Id);
           
            if (check_user != null)
            {
                return Ok("You`re already liked this film!");

            }
            
            Film currfilm = _db.Films.FirstOrDefault(x => x.Id == model.Id);
            UserFavoriteFilm favoriteFilm = new UserFavoriteFilm();
            favoriteFilm.UserId = user.Id;

            favoriteFilm.FilmId = currfilm.Id;         
            
            _db.UsersFavoriteFilms.Add(favoriteFilm);
            
            
            _db.SaveChanges();
            return Ok("You like this film");

        }
        /// <summary>
        /// добвление отзыва к фильму
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [Authorize]
        [Route("/review")]
        [HttpPost]
        public IActionResult Post(int filmId, string text)
        {
           
            var usrName = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.UserName == usrName);
            var check_user = _db.Reviews.Where(u => u.UserId == user.Id && u.FilmId == filmId);
           
            if (check_user != null) 
                
            {
                return Ok("You already commented this film!");

            }

            Review review = new Review();
            review.FilmId = filmId;
            review.UserId = user.Id; 
            review.Text = text;
            _db.Reviews.Add(review);
            _db.SaveChanges();
            return Ok("Comment added");
            
        }
        

        //[HttpGet]        
        //[Route("/review")]
        //public IActionResult Get(int filmId)
        //{
        //    var listOfReviews = _db.Reviews.Select(x => x.FilmId == filmId);
        //    if (listOfReviews == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(listOfReviews);

        //}

    }
    

    
}
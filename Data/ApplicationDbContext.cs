using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestFilms.Models;

namespace TestFilms.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public virtual DbSet<IdentityUser> User { get; set; }
        
        public DbSet<Film> Films { get; set; }
        public DbSet<UserFavoriteFilm> UsersFavoriteFilms { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Film>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Review>()
                .HasKey(bc => new { bc.UserId, bc.FilmId });
            modelBuilder.Entity<Review>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Reviews)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<Review>()
                .HasOne(bc => bc.Film)
                .WithMany(c => c.Reviews)
                .HasForeignKey(bc => bc.FilmId);

            modelBuilder.Entity<UserFavoriteFilm>()
                .HasKey(bc => new { bc.UserId, bc.FilmId });
            modelBuilder.Entity<UserFavoriteFilm>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UsersFavoriteFilms)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserFavoriteFilm>()
                .HasOne(bc => bc.Film)
                .WithMany(c => c.UsersFavoriteFilms)
                .HasForeignKey(bc => bc.FilmId);

            //modelBuilder.Entity<Review>()
            //    .HasKey(x=>x.Id);
            //modelBuilder.Entity<Review>().HasOne(x=>x.User).WithMany(x=>x.Reviews);
            //modelBuilder.Entity<Review>().HasOne(x => x.Film).WithMany(x => x.Reviews).HasForeignKey(x => x.FilmId);
            //modelBuilder.Entity<UserFavoriteFilm>().HasOne(x => x.User).WithMany(x => x.UsersFavoriteFilms);
            //modelBuilder.Entity<UserFavoriteFilm>().HasOne(x => x.Film).WithMany(x => x.UsersFavoriteFilms);


            //modelBuilder.Entity<User>().HasMany(user => user.FavoriteFilms);

            //modelBuilder.Entity<Film>().HasMany(film => film.UserWhoLikeIt);
            //modelBuilder.Entity<Film>().HasMany(film => film.AllReviews);

        }

    }
}

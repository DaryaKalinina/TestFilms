using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using TestFilms.Models;


namespace TestFilms.Data
{
    public static class AppDbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
                       
            if (context.Films.Any())
            {
                return;
            }
            var films = new Film[]
            {
                new Film{ Name ="Лобстер(The Lobster)", Genre ="триллер", Director = "Йоргос Лантимос", Actor="Колин Фаррелл" },
                new Film{ Name ="Джентльмены(The Gentlemen)", Genre ="боевик", Director ="Гай Ричи", Actor="Мэттью МакКонахи" },
                new Film{ Name ="Человек-невидимка(The Invisible Man)", Genre ="фантастика", Director ="Ли Уоннелл", Actor="Элизабет Мосс" },
                new Film{ Name ="Соник в кино(Sonic the Hedgehog)", Genre ="приключения", Director ="Джефф Фаулер", Actor="Джеймс Марсден" },

            };
            foreach (Film f in films)
            {
                context.Films.Add(f);
            }
            context.SaveChanges();
        }
       
    }
}

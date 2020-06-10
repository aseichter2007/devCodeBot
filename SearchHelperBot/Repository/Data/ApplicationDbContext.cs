using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        // member variables
        public DbSet<ActiveProject> ActiveProjects { get; set; }
        public DbSet<BadPhrase> BadPhrases { get; set; }
        public DbSet<BadWord> BadWords { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<NearConceptIdea> NearConceptIdeas { get; set; }
        public DbSet<NearConceptPhrase> NearConceptPhrases { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<PreferredLanguage> PreferredLanguages { get; set; }
        public DbSet<RawSearch> RawSearches { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<PreferredSearch> PreferredSearches { get; set; }

        // constructor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        // member methods
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<ActiveProject>()
        //        .HasData(
        //            new ActiveProject { WeekDayId = -1, Day = "" },
        //            new WeekDay { WeekDayId = 1, Day = "Monday" },
        //            new WeekDay { WeekDayId = 2, Day = "Tuesday" },
        //            new WeekDay { WeekDayId = 3, Day = "Wednesday" },
        //            new WeekDay { WeekDayId = 4, Day = "Thursday" },
        //            new WeekDay { WeekDayId = 5, Day = "Friday" },
        //            new WeekDay { WeekDayId = 6, Day = "Saturday" },
        //            new WeekDay { WeekDayId = 7, Day = "Sunday" }
        //        );
        //}
    }
}

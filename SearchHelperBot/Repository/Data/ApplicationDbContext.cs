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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActiveProject>()
                .HasData(
                    new ActiveProject { ActiveProjectId = 1, ProjectType = "asp.net mvc", Day = 0 }
                );
            builder.Entity<BadPhrase>()
                .HasData(
                    new BadPhrase { BadPhraseId = 1, Phrase = "trying to" },
                    new BadPhrase { BadPhraseId = 2, Phrase = "want to" },
                    new BadPhrase { BadPhraseId = 3, Phrase = "need to" }
                );
            builder.Entity<BadWord>()
                .HasData(
                    new BadWord { BadWordId = 1, Word = "i" },
                    new BadWord { BadWordId = 2, Word = "my" },
                    new BadWord { BadWordId = 3, Word = "am" },
                    new BadWord { BadWordId = 4, Word = "like" },     
                    new BadWord { BadWordId = 5, Word = "maybe" }
                );
            builder.Entity<Language>()
                .HasData(
                    new Language { LanguageId = 1, LanguageName = "c#" },
                    new Language { LanguageId = 2, LanguageName = "javascript" },
                    new Language { LanguageId = 3, LanguageName = "html" }
                );
            builder.Entity<NearConceptIdea>()
                .HasData(
                    new NearConceptIdea { NearConceptIdeaId = 1, ProperForm = "loop over each value", Day = 0 },
                    new NearConceptIdea { NearConceptIdeaId = 2, ProperForm = "find index of", Day = 0 },
                    new NearConceptIdea { NearConceptIdeaId = 3, ProperForm = "mvc view", Day = 0 }
                );
            builder.Entity<NearConceptPhrase>()
                .HasData(
                    new NearConceptPhrase { NearConceptPhraseId = 1, Phrase = "get values in", ConceptId = 1 },
                    new NearConceptPhrase { NearConceptPhraseId = 2, Phrase = "search for value", ConceptId = 1 },
                    new NearConceptPhrase { NearConceptPhraseId = 3, Phrase = "get individual value", ConceptId = 2 },
                    new NearConceptPhrase { NearConceptPhraseId = 4, Phrase = "find location of", ConceptId = 2 },
                    new NearConceptPhrase { NearConceptPhraseId = 5, Phrase = "webpage in mvc", ConceptId = 3 }
                );
            builder.Entity<Platform>()
                .HasData(
                    new Platform { PlatformId = 1, PlatformName = ".net" },
                    new Platform { PlatformId = 2, PlatformName = ".net core" },
                    new Platform { PlatformId = 3, PlatformName = ".net mvc" },
                    new Platform { PlatformId = 4, PlatformName = "mvc" }
                );
            builder.Entity<PreferredLanguage>()
                .HasData(
                    new PreferredLanguage { PreferredLanguageId = 1, LanguageName = "c#", Day = 0 }
                );
            builder.Entity<PreferredSearch>()
                .HasData(
                    new PreferredSearch { PreferredSearchId = 1, SearchName = "docs.microsoft.com" },
                    new PreferredSearch { PreferredSearchId = 2, SearchName = "stackoverflow" },
                    new PreferredSearch { PreferredSearchId = 3, SearchName = "w3schools" }
                );
            builder.Entity<Setting>()
                .HasData(
                    new Setting { SettingId = 1, SettingName = "logging", Set = false }
                );
        }
    }
}
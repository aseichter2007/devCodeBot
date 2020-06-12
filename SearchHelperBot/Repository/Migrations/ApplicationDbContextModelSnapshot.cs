﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Data;

namespace Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Repository.Models.ActiveProject", b =>
                {
                    b.Property<int>("ActiveProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("ProjectType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActiveProjectId");

                    b.ToTable("ActiveProjects");

                    b.HasData(
                        new
                        {
                            ActiveProjectId = 1,
                            Day = 0,
                            ProjectType = "asp.net mvc"
                        });
                });

            modelBuilder.Entity("Repository.Models.BadPhrase", b =>
                {
                    b.Property<int>("BadPhraseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Phrase")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BadPhraseId");

                    b.ToTable("BadPhrases");

                    b.HasData(
                        new
                        {
                            BadPhraseId = 1,
                            Phrase = "trying to"
                        },
                        new
                        {
                            BadPhraseId = 2,
                            Phrase = "want to"
                        },
                        new
                        {
                            BadPhraseId = 3,
                            Phrase = "need to"
                        });
                });

            modelBuilder.Entity("Repository.Models.BadWord", b =>
                {
                    b.Property<int>("BadWordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BadWordId");

                    b.ToTable("BadWords");

                    b.HasData(
                        new
                        {
                            BadWordId = 1,
                            Word = "i"
                        },
                        new
                        {
                            BadWordId = 2,
                            Word = "my"
                        },
                        new
                        {
                            BadWordId = 3,
                            Word = "am"
                        },
                        new
                        {
                            BadWordId = 4,
                            Word = "like"
                        },
                        new
                        {
                            BadWordId = 5,
                            Word = "maybe"
                        });
                });

            modelBuilder.Entity("Repository.Models.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LanguageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            LanguageId = 1,
                            LanguageName = "c#"
                        },
                        new
                        {
                            LanguageId = 2,
                            LanguageName = "javascript"
                        },
                        new
                        {
                            LanguageId = 3,
                            LanguageName = "html"
                        });
                });

            modelBuilder.Entity("Repository.Models.NearConceptIdea", b =>
                {
                    b.Property<int>("NearConceptIdeaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("ProperForm")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NearConceptIdeaId");

                    b.ToTable("NearConceptIdeas");

                    b.HasData(
                        new
                        {
                            NearConceptIdeaId = 1,
                            Day = 0,
                            ProperForm = "loop over each value"
                        },
                        new
                        {
                            NearConceptIdeaId = 2,
                            Day = 0,
                            ProperForm = "find index of"
                        },
                        new
                        {
                            NearConceptIdeaId = 3,
                            Day = 0,
                            ProperForm = "mvc view"
                        });
                });

            modelBuilder.Entity("Repository.Models.NearConceptPhrase", b =>
                {
                    b.Property<int>("NearConceptPhraseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConceptId")
                        .HasColumnType("int");

                    b.Property<string>("Phrase")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NearConceptPhraseId");

                    b.ToTable("NearConceptPhrases");

                    b.HasData(
                        new
                        {
                            NearConceptPhraseId = 1,
                            ConceptId = 1,
                            Phrase = "get values in"
                        },
                        new
                        {
                            NearConceptPhraseId = 2,
                            ConceptId = 1,
                            Phrase = "search for value"
                        },
                        new
                        {
                            NearConceptPhraseId = 3,
                            ConceptId = 2,
                            Phrase = "get individual value"
                        },
                        new
                        {
                            NearConceptPhraseId = 4,
                            ConceptId = 2,
                            Phrase = "find location of"
                        },
                        new
                        {
                            NearConceptPhraseId = 5,
                            ConceptId = 3,
                            Phrase = "webpage in mvc"
                        });
                });

            modelBuilder.Entity("Repository.Models.Platform", b =>
                {
                    b.Property<int>("PlatformId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PlatformName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlatformId");

                    b.ToTable("Platforms");

                    b.HasData(
                        new
                        {
                            PlatformId = 1,
                            PlatformName = ".net"
                        },
                        new
                        {
                            PlatformId = 2,
                            PlatformName = ".net core"
                        },
                        new
                        {
                            PlatformId = 3,
                            PlatformName = ".net mvc"
                        },
                        new
                        {
                            PlatformId = 4,
                            PlatformName = "mvc"
                        });
                });

            modelBuilder.Entity("Repository.Models.PreferredLanguage", b =>
                {
                    b.Property<int>("PreferredLanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("LanguageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PreferredLanguageId");

                    b.ToTable("PreferredLanguages");

                    b.HasData(
                        new
                        {
                            PreferredLanguageId = 1,
                            Day = 0,
                            LanguageName = "c#"
                        });
                });

            modelBuilder.Entity("Repository.Models.PreferredSearch", b =>
                {
                    b.Property<int>("PreferredSearchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SearchName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PreferredSearchId");

                    b.ToTable("PreferredSearches");

                    b.HasData(
                        new
                        {
                            PreferredSearchId = 1,
                            SearchName = "docs.microsoft.com"
                        },
                        new
                        {
                            PreferredSearchId = 2,
                            SearchName = "stackoverflow"
                        },
                        new
                        {
                            PreferredSearchId = 3,
                            SearchName = "w3schools"
                        });
                });

            modelBuilder.Entity("Repository.Models.RawSearch", b =>
                {
                    b.Property<int>("RawSearchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Search")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RawSearchId");

                    b.ToTable("RawSearches");
                });

            modelBuilder.Entity("Repository.Models.Setting", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Set")
                        .HasColumnType("bit");

                    b.Property<string>("SettingName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SettingId");

                    b.ToTable("Settings");

                    b.HasData(
                        new
                        {
                            SettingId = 1,
                            Set = false,
                            SettingName = "logging"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

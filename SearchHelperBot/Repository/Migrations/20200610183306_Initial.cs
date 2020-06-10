using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveProjects",
                columns: table => new
                {
                    ActiveProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectType = table.Column<string>(nullable: true),
                    Day = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveProjects", x => x.ActiveProjectId);
                });

            migrationBuilder.CreateTable(
                name: "BadPhrases",
                columns: table => new
                {
                    BadPhraseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phrase = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadPhrases", x => x.BadPhraseId);
                });

            migrationBuilder.CreateTable(
                name: "BadWords",
                columns: table => new
                {
                    BadWordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadWords", x => x.BadWordId);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "NearConceptIdeas",
                columns: table => new
                {
                    NearConceptIdeaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProperForm = table.Column<string>(nullable: true),
                    Day = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NearConceptIdeas", x => x.NearConceptIdeaId);
                });

            migrationBuilder.CreateTable(
                name: "NearConceptPhrases",
                columns: table => new
                {
                    NearConceptPhraseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phrase = table.Column<string>(nullable: true),
                    ConceptId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NearConceptPhrases", x => x.NearConceptPhraseId);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    PlatformId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatformName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.PlatformId);
                });

            migrationBuilder.CreateTable(
                name: "PreferredLanguages",
                columns: table => new
                {
                    PreferredLanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(nullable: true),
                    Day = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferredLanguages", x => x.PreferredLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "PreferredSearches",
                columns: table => new
                {
                    PreferredSearchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferredSearches", x => x.PreferredSearchId);
                });

            migrationBuilder.CreateTable(
                name: "RawSearches",
                columns: table => new
                {
                    RawSearchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(nullable: true),
                    Search = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawSearches", x => x.RawSearchId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingName = table.Column<string>(nullable: true),
                    Set = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingId);
                });

            migrationBuilder.InsertData(
                table: "ActiveProjects",
                columns: new[] { "ActiveProjectId", "Day", "ProjectType" },
                values: new object[] { 1, 0, "asp.net mvc" });

            migrationBuilder.InsertData(
                table: "BadPhrases",
                columns: new[] { "BadPhraseId", "Phrase" },
                values: new object[,]
                {
                    { 1, "trying to" },
                    { 2, "want to" },
                    { 3, "need to" }
                });

            migrationBuilder.InsertData(
                table: "BadWords",
                columns: new[] { "BadWordId", "Word" },
                values: new object[,]
                {
                    { 1, "i" },
                    { 2, "my" },
                    { 3, "am" },
                    { 4, "like" },
                    { 5, "maybe" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorId", "UserName" },
                values: new object[,]
                {
                    { 1, "Brett Johnson" },
                    { 2, "Charles King" },
                    { 3, "David Lagrange" },
                    { 4, "Michael Heinisch" },
                    { 5, "Michael Terrill" },
                    { 6, "Nevin Seibel" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "LanguageName" },
                values: new object[,]
                {
                    { 3, "html" },
                    { 2, "javascript" },
                    { 1, "c#" }
                });

            migrationBuilder.InsertData(
                table: "NearConceptIdeas",
                columns: new[] { "NearConceptIdeaId", "Day", "ProperForm" },
                values: new object[,]
                {
                    { 1, 0, "loop over each value" },
                    { 2, 0, "find index of" },
                    { 3, 0, "mvc view" }
                });

            migrationBuilder.InsertData(
                table: "NearConceptPhrases",
                columns: new[] { "NearConceptPhraseId", "ConceptId", "Phrase" },
                values: new object[,]
                {
                    { 4, 2, "find location of" },
                    { 3, 2, "get individual value" },
                    { 5, 3, "webpage in mvc" },
                    { 1, 1, "get values in" },
                    { 2, 1, "search for value" }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "PlatformId", "PlatformName" },
                values: new object[,]
                {
                    { 1, ".net" },
                    { 2, ".net core" },
                    { 3, ".net mvc" },
                    { 4, "mvc" }
                });

            migrationBuilder.InsertData(
                table: "PreferredLanguages",
                columns: new[] { "PreferredLanguageId", "Day", "LanguageName" },
                values: new object[] { 1, 0, "c#" });

            migrationBuilder.InsertData(
                table: "PreferredSearches",
                columns: new[] { "PreferredSearchId", "SearchName" },
                values: new object[,]
                {
                    { 2, "stackoverflow" },
                    { 1, "docs.microsoft.com" },
                    { 3, "w3schools" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveProjects");

            migrationBuilder.DropTable(
                name: "BadPhrases");

            migrationBuilder.DropTable(
                name: "BadWords");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "NearConceptIdeas");

            migrationBuilder.DropTable(
                name: "NearConceptPhrases");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "PreferredLanguages");

            migrationBuilder.DropTable(
                name: "PreferredSearches");

            migrationBuilder.DropTable(
                name: "RawSearches");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}

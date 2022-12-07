using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Domain.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    User = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    User = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieReviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "IsEnabled", "Name", "ReleaseDate", "Type", "UpdatedAt", "User" },
                values: new object[] { 1, "Nick Fury is compelled to launch the Avengers Initiative whenLoki poses a threat to planet Earth. His squad of superheroes put their minds together to accomplish the task.", false, "The Avengers", new DateTime(2012, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adventure/Action", new DateTime(2022, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "mtsai" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "IsEnabled", "Name", "ReleaseDate", "Type", "UpdatedAt", "User" },
                values: new object[] { 2, "Queen Ramonda, Shuri, M'Baku, Okoye and the Dora Milaje fight to protect their nation from intervening world powers in the wake of King T'Challa's death. As the Wakandans strive to embrace their next chapter, the heroes must band together with Nakia and Everett Ross to forge a new path for their beloved kingdom", true, "Black Panther: Wakanda Forever", new DateTime(2022, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adventure/Action", new DateTime(2022, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "mtsai" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieReviews_MovieId",
                table: "MovieReviews",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieReviews");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}

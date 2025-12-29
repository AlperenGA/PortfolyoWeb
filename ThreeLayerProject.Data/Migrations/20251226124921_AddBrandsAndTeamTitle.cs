using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThreeLayerProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandsAndTeamTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "SocialMedias",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "SiteSettings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "Roles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "Projects",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "ProjectComments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "ContactMessages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "ContactInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "Blogs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "BlogComments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamSectionTitle",
                table: "AboutMe",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    TeamSectionTitle = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "ProjectComments");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "TeamSectionTitle",
                table: "AboutMe");
        }
    }
}

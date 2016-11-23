using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DungeonBuilder.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    DungeonComponentId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Height = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    x = table.Column<int>(nullable: false),
                    y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.DungeonComponentId);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    DungeonLevelId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.DungeonLevelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Levels");
        }
    }
}

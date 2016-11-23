using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DungeonBuilder.Migrations
{
    public partial class MyThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
	    8*shmigrationBuilder.DropColumn(
                name: "LevelId",
                table: "Components");
	    */
            migrationBuilder.AddColumn<int>(
                name: "DungeonLevelId",
                table: "Components",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DungeonLevelId",
                table: "Components");

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Components",
                nullable: false,
                defaultValue: 0);
        }
    }
}

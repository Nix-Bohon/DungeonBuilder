using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DungeonBuilder.Migrations
{
    [DbContext(typeof(DungeonContext))]
    partial class DungeonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("DungeonBuilder.Models.DungeonComponent", b =>
                {
                    b.Property<int>("DungeonComponentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DungeonLevelId");

                    b.Property<int>("Height");

                    b.Property<int>("Width");

                    b.Property<int>("x");

                    b.Property<int>("y");

                    b.HasKey("DungeonComponentId");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("DungeonBuilder.Models.DungeonLevel", b =>
                {
                    b.Property<int>("DungeonLevelId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("DungeonLevelId");

                    b.ToTable("Levels");
                });
        }
    }
}

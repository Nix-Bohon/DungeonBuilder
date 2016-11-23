using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using DungeonBuilder.Models;

public class DungeonContext : DbContext
{
    // public DbSet<Blog> Blogs { get; set; }
    // public DbSet<Post> Posts { get; set; }
    public DbSet<DungeonLevel> Levels {get;set;}
    public DbSet<DungeonComponent> Components {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=./blog.db");
    }
}

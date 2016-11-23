using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DungeonBuilder.Models;
using DungeonBuilder.Factories;

namespace DungeonBuilder.Controllers
{
    [Route("api/[controller]")]
    public class LevelController : Controller
    {
       	[HttpGet()]
	public DungeonLevel GetNew()
	{
	    var dungeonFactory = new DungeonLevelCreator();
	    return dungeonFactory.GenerateLevel();
	}
	[HttpPost]
	public void Post([FromBody] DungeonLevel level)
	{
	    using(var db = new DungeonContext())
	    {
		db.Levels.Add(level);
		db.SaveChanges();
		Console.WriteLine($"DungeonLevelId: {level.DungeonLevelId}");
		foreach(DungeonComponent c in level.Components)
		{
		    c.DungeonLevelId = level.DungeonLevelId;
		}
		db.Components.AddRange(level.Components);
		db.SaveChanges();
	    }
	}
	[HttpGet("{dungeonLevelId}")]
	public DungeonLevel Get(int dungeonLevelId)
	{
	    using(var db = new DungeonContext())
	    {
		var level = db.Levels
		    .Single(l => l.DungeonLevelId == dungeonLevelId);
		level.Components = db.Components
		    .Where(c => c.DungeonLevelId == dungeonLevelId)
		    .ToList();
		return level;
	    }
	}
    }
}

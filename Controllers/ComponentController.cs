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
    public class ComponentController : Controller
    {
	[HttpGet("{componentId}")]
	public DungeonComponent Get(int componentId)
	{
	    using(var db = new DungeonContext())
	    {
		var component = db.Components
		    .Single(c => c.DungeonComponentId == componentId);
		return component;
	    }
	}
	[HttpPost]
	public int Post([FromBody] DungeonComponent component)
	{
	    using(var db = new DungeonContext())
	    {
		db.Components.Add(component);
		db.SaveChanges();
		return component.DungeonComponentId;
	    }
	}
	[HttpPut]
	public void Put([FromBody] DungeonComponent component)
	{
	    using(var db = new DungeonContext())
	    {
		var comp = db.Components
		    .Single(c => c.DungeonComponentId == component.DungeonComponentId);
		comp = component;
		db.SaveChanges();
	    }
	}
    }
}

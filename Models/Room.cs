using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBuilder.Models
{
    public class Room:DungeonComponent
    {
        public List<Doorway> Doors = new List<Doorway>();
        public Room(int x, int y, int height, int width):base(x,y,height,width) {}
        public Room() : base() { }

        public bool hasDoorAt(int x, int y)
        {
            return Doors.Any(item => item.x == x && item.y == y);
        }
    }

    public class Chamber:Room {}
}

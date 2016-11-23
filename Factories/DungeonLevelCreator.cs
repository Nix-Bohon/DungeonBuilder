using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Models;

namespace DungeonBuilder.Factories
{
    public class DungeonLevelCreator
    {
        private RoomGenerator _RoomGenerator;
        public DungeonLevelCreator()
        {
            _RoomGenerator = new RoomGenerator();
        }
        public DungeonLevel GenerateLevel()
        {
            DungeonLevel level = new DungeonLevel();
            SetupLevelSeed(level);
            List<Doorway> unexpandedDoors = new List<Doorway>();
            unexpandedDoors = level.GetUnexpandedDoors();
            while (unexpandedDoors.Count > 0 && level.Components.Count < 40)
            {
                foreach (Doorway entrance in unexpandedDoors)
                {
                    if (ExpandDungeonLevel(level, entrance))
                        entrance.connected = true;
                    else
                        level.Remove(entrance);
                }
                unexpandedDoors = level.GetUnexpandedDoors();
            } 
            return level;
        }

        /// <summary>
        /// Determines what is behind a door.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="door"></param>
        /// <returns></returns>
        /// <remarks>For now this just calls the RoomGenerator, but once I've added chambers and passageways it will be a bit more complicated.</remarks>
        private bool ExpandDungeonLevel(DungeonLevel level, Doorway door)
        {
            return _RoomGenerator.GenerateComponent(level, door);
        }
        /// <summary>
        /// Can be overridden to create different dungeon seeds.
        /// </summary>
        /// <param name="level"></param>
        protected void SetupLevelSeed(DungeonLevel level)
        {
            level.Add(new Room(0,0,5,5));
            level.Add(new Doorway(0, 3, 1, 0, Side.West));
            level.Add(new Doorway(5, 3, 1, 0, Side.East));
            level.Add(new Doorway(3, 0, 0, 1, Side.South));
            level.Add(new Doorway(3, 5, 0, 1, Side.North));
        }
    }
}

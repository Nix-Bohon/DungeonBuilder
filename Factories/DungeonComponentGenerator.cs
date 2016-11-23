using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Models;

namespace DungeonBuilder.Factories
{
    abstract class DungeonComponentGenerator<component> where component : DungeonComponent
    {
        public DungeonComponentGenerator()
        {

        }
        /// <summary>
        /// Factory method implemented by concrete classes.
        /// </summary>
        /// <param name="Entrance"></param>
        /// <returns></returns>
        protected abstract component GetComponent(Doorway Entrance);

        /// <summary>
        /// Used to reduce the size of a component which is too big to fit on the current map.
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns>false if component is already as small as possible.</returns>
        protected abstract bool tryReduceSize(component candidate);

        /// <summary>
        /// Should be implemented to add exits and any other elements which are not related to the space filling attributes of the component.
        /// </summary>
        /// <param name="Level"></param>
        /// <param name="Entrance"></param>
        /// <param name="component"></param>
        /// <returns>False if failed for some reason.</returns>
        protected abstract bool tryAddDetails(DungeonLevel Level, Doorway Entrance, component component);

        /// <summary>
        /// This method attempts to find a way to fit the candidate into the level without colliding with any existing components.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="entrance"></param>
        /// <param name="candidate"></param>
        /// <returns>false if the component could not be placed.</returns>
        protected bool tryPlaceComponent(DungeonLevel level, Doorway entrance, component candidate)
        {
            switch (entrance.SideIn)
            {
                case Side.North:
                    candidate.y = entrance.y;
                    break;
                case Side.South:
                    candidate.y = entrance.y - candidate.Height;
                    break;
                case Side.West:
                    candidate.x = entrance.x - candidate.Width;
                    break;
                case Side.East:
                    candidate.x = entrance.x;
                    break;
            }
            IList<int> potentialOrigins = new List<int>();
            if (entrance.SideIn == Side.North || entrance.SideIn == Side.South)
            {
                for (int i = entrance.x - candidate.Width; i < entrance.x; i++) { potentialOrigins.Add(i + 1); }
                while (potentialOrigins.Count > 0)
                {
                    int i = DiceBag.getDice().rand.Next(potentialOrigins.Count);
                    candidate.x = potentialOrigins[i];
                    if (level.Add(candidate)) { return true; }
                    potentialOrigins.RemoveAt(i);
                }
            }
            else
            {
                for (int i = entrance.y - candidate.Height; i < entrance.y; i++) { potentialOrigins.Add(i + 1); }
                while (potentialOrigins.Count > 0)
                {
                    int i = DiceBag.getDice().rand.Next(potentialOrigins.Count);
                    candidate.y = potentialOrigins[i];
                    if (level.Add(candidate)) { return true; }
                    potentialOrigins.RemoveAt(i);
                }
            }
            return false;
        }

        public bool GenerateComponent(DungeonLevel level, Doorway entrance)
        {
            component candidate = GetComponent(entrance);
            while (tryPlaceComponent(level, entrance, candidate) == false)
                if (tryReduceSize(candidate) == false)
                    return false;
            return tryAddDetails(level, entrance, candidate);
        }
    }
}

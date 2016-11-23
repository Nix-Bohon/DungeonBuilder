using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBuilder.Models
{
    class Passageway:DungeonComponent
    {
        public Passageway(int x, int y, int height, int width):base(x,y,height,width) {}
        public Passageway(int x, int y, Side SideIn)
        {
            switch (SideIn)
            {
                case Side.North:
                    this.x = x;
                    this.y = y;
                    Height = 6;
                    Width = 2; 
                    break; 
                case Side.East:
                    this.x = x;
                    this.y = y;
                    Height = 2;
                    Width = 6;
                    break;
                case Side.South:
                    this.y = y - 6;
                    this.x = x;
                    this.Height = 6;
                    this.Width = 2;
                    break;
                case Side.West:
                    this.y = y;
                    this.x = x - 6;
                    this.Height = 2;
                    this.Width = 6;
                    break;
            }
        }
        public Passageway() : base() { }
    }
}

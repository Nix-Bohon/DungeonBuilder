using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBuilder.Models
{
    public class Doorway:DungeonComponent
    {
        public bool connected = false;
        public bool broken = false;
        public Side SideIn;
        //public Side SideOut { get { return (Side)((((int)SideIn) + (int)RelativeSide.Opposite) % 4); } }
        public DoorType Type;
        public Doorway(int x, int y, int height, int width) : base(x, y, height, width) { }
        public Doorway()
            : base(0, 0, 0, 1)
        {
            SideIn = Side.North;
            Type = DoorType.TwoWay;
        }
        public Doorway(int x, int y, int height, int width, Side SideIn)
            : base(x, y, height, width)
        {
            this.SideIn = SideIn;
            Type = DoorType.TwoWay;
        }
    }
    public enum Side { North=0, East=1, South=2, West=3 }
    public enum DoorType {Secret, TwoWay, OneWay }
    public enum RelativeSide { Same = 0, Left = 1, Opposite = 2, Right = 3 }
}

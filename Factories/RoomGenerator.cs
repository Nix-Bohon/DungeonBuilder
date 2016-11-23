using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Models;

namespace DungeonBuilder.Factories
{
    class RoomGenerator : DungeonComponentGenerator<Models.Room>
    {
        protected override Models.Room GetComponent(Models.Doorway Entrance)
        {
            Models.Room r = new Models.Room(0, 0, 1, 1);
            switch (Models.DiceBag.getDice().d20()) // TABLE V
            {
                case 1:
                case 2: // 1-2 Square 10' x 10'
                    r.Height = 2;
                    r.Width = 2;
                    break;
                case 3:
                case 4: // 3-4 Square 20' x 20'
                    r.Height = 4;
                    r.Width = 4;
                    break;
                case 5:
                case 6: // 5-6 Square 30' x 30'
                    r.Height = 6;
                    r.Width = 6;
                    break;
                case 7:
                case 8: // 7-8 Square 40' x 40'
                    r.Height = 8;
                    r.Width = 8;
                    break;
                case 9:
                case 10: // 9-10 Rectangle 10' x 20'
                    r.Height = 2;
                    r.Width = 4;
                    break;
                case 11:
                case 12:
                case 13: // 11-13 Rectangle 20' x 30'
                    r.Height = 4;
                    r.Width = 6;
                    break;
                case 14:
                case 15: // 14-15 Rectangle 20' x 40'
                    r.Height = 4;
                    r.Width = 8;
                    break;
                case 16:
                case 17: // 116-17 Rectangle 30' x 40'
                    r.Height = 6;
                    r.Width = 8;
                    break;
                case 18:
                case 19:
                case 20: // Unusual shape and size
                    // TODO implement handling for unusual shapes.
                    r.Height = 6;
                    r.Width = 4;
                    break;
            }
            return r;
        }

        protected override bool tryReduceSize(Models.Room candidate)
        {
            if (candidate.Height >= candidate.Width)
            {
                candidate.Height--;
            }
            else 
            {
                candidate.Flip();
            }
            return candidate.Height > 1 && candidate.Width > 1;
        }

        protected override bool tryAddDetails(Models.DungeonLevel Level, Models.Doorway Entrance, Models.Room room)
        {
            int numberOfExits = RollNumberOfExits(room.Area);
            if (numberOfExits == 0)
                addHiddenExits(Level, room);
            for (int i = 0; i < numberOfExits; i++)
            {
                Doorway exit = new Doorway();
                do
                {
                    exit.SideIn = RollExitSide(Entrance.SideIn);
                    if (exit.SideIn == Side.North || exit.SideIn == Side.South)
                    {
                        exit.Height = 0;
                        exit.Width = 1;
                        if (exit.SideIn == Side.North)
                            exit.y = room.getYEdge();
                        else
                            exit.y = room.y;
                        exit.x = DiceBag.getDice().rand.Next(room.x, room.getXEdge());
                    }
                    else
                    {
                        exit.Width = 0;
                        exit.Height = 1;
                        if (exit.SideIn == Side.East)
                            exit.x = room.getXEdge();
                        else
                            exit.x = room.x;
                        exit.y = DiceBag.getDice().rand.Next(room.y, room.getYEdge());
                    }
                } while (room.hasDoorAt(exit.x, exit.y) && !FindDoorType(Level, exit));
                room.Doors.Add(exit);
                Level.Add(exit);
            }
            return true;
        }
        private void addHiddenExits(DungeonLevel level, Room room)
        {
            // todo implement this;
        }
        private Side RollExitSide(Side entrance)
        {
            int roll = DiceBag.getDice().d20(); // TABL V.D
            RelativeSide rel = RelativeSide.Same;
            if (roll <= 7)                      // 1-7 opposite wall
                rel = RelativeSide.Opposite;
            else if (roll <= 12)                // 8-12 left wall
                rel = RelativeSide.Left;
            else if (roll <= 17)                // 13-17 right wall
                rel = RelativeSide.Right;
            return (Side)((((int)entrance + (int)rel) % 4));
        }
        private int RollNumberOfExits(int RoomArea)
        {
            int numberOfExits = 0;
            switch (DiceBag.getDice().d20()) // TABLE V.C
            {
                case 1:
                case 2:
                case 3: // 1-3 up to 600' 1; over 600' 2
                    if (RoomArea > 24)
                        numberOfExits = 2;
                    else
                        numberOfExits = 1;
                    break;
                case 4:
                case 5:
                case 6: // 4-6 up to 600' 2; over 600' 3
                    if (RoomArea > 24)
                        numberOfExits = 3;
                    else
                        numberOfExits = 2;
                    break;
                case 7:
                case 8:
                case 9: // 7-9 up to 600' 3; over 600' 4
                    if (RoomArea > 600)
                        numberOfExits = 4;
                    else
                        numberOfExits = 3;
                    break;
                case 10:
                case 11:
                case 12: // 10-12 up to 1200' 0*; over 1200' 1
                    if (RoomArea > 48)
                        numberOfExits = 1;
                    else
                        numberOfExits = 0;
                    break;
                case 13:
                case 14:
                case 15: // up to 1600' 0*; over 1600' 1
                    if (RoomArea > 64)
                        numberOfExits = 1;
                    else
                        numberOfExits = 0;
                    break;
                case 16:
                case 17:
                case 18: // any size 1-4 (d4)
                    numberOfExits = DiceBag.getDice().d4();
                    break;
                case 19:
                case 20: // any size 1
                    numberOfExits = 1;
                    break;
            }
            return numberOfExits;
        }
        private bool FindDoorType(DungeonLevel level, Doorway door)
        {
            DungeonComponent tester = new DungeonComponent(door.x, door.y, 1, 1);
            switch (door.SideIn)
            {
                case Side.South:
                    tester.y--;
                    break;
                case Side.West:
                    tester.x--;
                    break;
            }
            if (level.Collision(tester))
            {
                int roll = DiceBag.getDice().d20();
                if (roll > 10)
                    return false;
                else
                {
                    door.connected = true;
                    if (roll > 5)
                        door.Type = DoorType.OneWay;
                    else
                        door.Type = DoorType.Secret;
                }
            }
            else
            {
                door.connected = false;
                door.Type = DoorType.TwoWay;
            }
            return true;
        }
    }
}

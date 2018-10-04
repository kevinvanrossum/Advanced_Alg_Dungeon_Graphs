using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public class Dungeon : IDungeon, IPrintable
    {
        public int XSize { get; set; }
        public int YSize { get; set; }
        public IRoom StartRoom { get; set; }
        public IRoom EndRoom { get; set; }
        public List<IRoom> Rooms { get; set; }
        public List<IHallway> Hallways { get; set; }

        public Dungeon()
        {
            Rooms = new List<IRoom>();
            Hallways = new List<IHallway>();
        }

        public Dungeon(int xSize, int ySize) : this()
        {
            XSize = xSize;
            YSize = ySize;
        }

        public Dungeon(IRoom startRoom, IRoom endRoom, int xSize, int ySize) : this(xSize, ySize)
        {
            StartRoom = startRoom;
            EndRoom = endRoom;
        }

        public void AddRoom(IRoom room)
        {
            Rooms.Add(room);
        }

        public void AddHallway(IHallway hallway)
        {
            Hallways.Add(hallway);
        }

        public IRoom GetRoom(int x, int y)
        {
            return Rooms.FirstOrDefault(room => room.X == x && room.Y == y);
        }

        public string ToPrintable()
        {
            var result = "";
            for (var y = 0; y < YSize; y++)
            {
                for (var x = 0; x < XSize; x++)
                {
                    var currentRoom = (Room) GetRoom(x, y);
                    result += $"{currentRoom.ToPrintable()}";
                    if (currentRoom.X >= XSize - 1) continue;
                    var hallway = (Hallway) currentRoom.AdjacentHallways.First(h => h.RoomA == currentRoom);
                    result += $" {hallway.ToPrintable()} ";
                }

                result += Environment.NewLine;

                for (var x = 0; x < XSize; x++)
                {
                    var currentRoom = (Room) GetRoom(x, y);
                    if (currentRoom.Y >= YSize - 1) continue;
                    var hallway = (Hallway) currentRoom.AdjacentHallways.Last(h => h.RoomA == currentRoom);
                    result += $"{hallway.ToPrintable()}   ";
                }

                result += Environment.NewLine;
            }

            return result;
        }
    }
}
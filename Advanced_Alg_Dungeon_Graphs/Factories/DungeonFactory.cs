using Advanced_Alg_Dungeon_Graphs.Models;
using System;

namespace Advanced_Alg_Dungeon_Graphs.Factories
{
    public class DungeonFactory : IDungeonFactory
    {
        private readonly IRoomFactory _iRoomFactory;
        private readonly IHallwayFactory _iHallwayFactory;

        public DungeonFactory(IRoomFactory iRoomFactory, IHallwayFactory iHallwayFactory)
        {
            _iRoomFactory = iRoomFactory;
            _iHallwayFactory = iHallwayFactory;
        }

        public IDungeon Create()
        {
            const int xSize = 5;
            const int ySize = 5;
            var dungeon = new Dungeon(5, 5);

            AddRoomsToDungeon(xSize, ySize, dungeon);

            return dungeon;
        }

        public IDungeon CreateWithXSizeAndYSize(int x, int y)
        {
            var dungeon = new Dungeon(x, y);

            AddRoomsToDungeon(x, y, dungeon);
            ConnectRoomsInDungeonWithHallways(dungeon);

            return dungeon;
        }

        private void ConnectRoomsInDungeonWithHallways(Dungeon dungeon)
        {
            for (var x = 0; x < dungeon.XSize; x++)
            {
                for (var y = 0; y < dungeon.YSize; y++)
                {
          /*
          var currentRoom = dungeon.GetRoom(x, y);
          var adjacentRoomX = dungeon.GetRoom(x + 1, y);
          var adjacentRoomY = dungeon.GetRoom(x, y + 1);

          SetRoomHallwayAdjacency(dungeon, adjacentRoomX, currentRoom);
          SetRoomHallwayAdjacency(dungeon, adjacentRoomY, currentRoom);
          */

          var currentRoom = dungeon.GetRoom(x, y);
          var adjacentRoomN = dungeon.GetRoom(x, y - 1);
          var adjacentRoomE = dungeon.GetRoom(x + 1, y);
          var adjacentRoomS = dungeon.GetRoom(x, y + 1);
          var adjacentRoomW = dungeon.GetRoom(x - 1, y);
          try
          {
            SetRoomHallwayAdjacency(dungeon, adjacentRoomN, currentRoom);
            SetRoomHallwayAdjacency(dungeon, adjacentRoomE, currentRoom);
            SetRoomHallwayAdjacency(dungeon, adjacentRoomS, currentRoom);
            SetRoomHallwayAdjacency(dungeon, adjacentRoomW, currentRoom);
          }
          catch (NullReferenceException e)
          {
            Console.WriteLine(e.Message);
          }
        }
            }
        }

        private void SetRoomHallwayAdjacency(Dungeon dungeon, IRoom adjacentRoomY, IRoom currentRoom)
        {
            if (adjacentRoomY == null) return;
            var hallway = _iHallwayFactory.CreateWithRooms(currentRoom, adjacentRoomY);
            dungeon.AddHallway(hallway);
            currentRoom.AddAdjacentHallway(hallway);
        }

        private void AddRoomsToDungeon(int xSize, int ySize, IDungeon dungeon)
        {
            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0; y < ySize; y++)
                {
                    dungeon.AddRoom(_iRoomFactory.CreateWithCoordinates(x, y));
                }
            }
        }
    }
}

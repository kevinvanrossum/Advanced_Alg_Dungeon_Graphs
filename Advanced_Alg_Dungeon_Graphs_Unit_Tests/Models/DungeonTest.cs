using System.Collections.Generic;
using System.Linq;
using Advanced_Alg_Dungeon_Graphs.Models;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Models
{
    public class DungeonTest
    {
        private readonly Dungeon _dungeon;

        public DungeonTest()
        {
            _dungeon = new Dungeon();
        }

        [Fact]
        public void ItStoresXSizeProperly()
        {
            const int xSize = 5;

            _dungeon.XSize = xSize;
            
            Assert.Equal(xSize, _dungeon.XSize);
        }
        [Fact]
        public void ItStoresYSizeProperly()
        {
            const int ySize = 5;

            _dungeon.YSize = ySize;
            
            Assert.Equal(ySize, _dungeon.YSize);
        }
        
        [Fact]
        public void ItStoresStartRoomProperly()
        {
            var room = new Room();

            _dungeon.StartRoom = room;
            
            Assert.Equal(room, _dungeon.StartRoom);
        }
        
        [Fact]
        public void ItStoresRoomsProperly()
        {
            var rooms = new List<IRoom>();

            _dungeon.Rooms = rooms;
            
            Assert.Equal(rooms, _dungeon.Rooms);
        }   
        
        [Fact]
        public void ItStoresHallwaysProperly()
        {
            var hallways = new List<IHallway>();

            _dungeon.Hallways = hallways;
            
            Assert.Equal(hallways, _dungeon.Hallways);
        }

        [Fact]
        public void ItCanAddRooms()
        {
            var room = new Room();
            
            _dungeon.AddRoom(room);
            
            Assert.Equal(room, _dungeon.Rooms.First());
        }

        [Fact]
        public void ItCanAddHallways()
        {
            var hallway = new Hallway();
            
            _dungeon.AddHallway(hallway);
            
            Assert.Equal(hallway, _dungeon.Hallways.First());
        }
    }
}
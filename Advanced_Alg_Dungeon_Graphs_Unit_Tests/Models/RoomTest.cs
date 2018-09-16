using System.Collections.Generic;
using Advanced_Alg_Dungeon_Graphs.Models;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Models
{
    public class RoomTest
    {
        private readonly Room _room;

        public RoomTest()
        {
            _room = new Room();
        }

        [Fact]
        public void ItStoresXProperly()
        {
            const int x = 0;
            
            _room.X = x;
            
            Assert.Equal(x, _room.X);
        }
        
        [Fact]
        public void ItStoresYProperly()
        {
            const int y = 0;
            
            _room.Y = y;
            
            Assert.Equal(y, _room.Y);
        }

        [Fact]
        public void ItStoresAdjacentHallwaysProperly()
        {
            var adjacentHalways = new List<IHallway>();

            _room.AdjacentHallways = adjacentHalways;
            
            Assert.Equal(adjacentHalways, _room.AdjacentHallways);
        }
    }
}
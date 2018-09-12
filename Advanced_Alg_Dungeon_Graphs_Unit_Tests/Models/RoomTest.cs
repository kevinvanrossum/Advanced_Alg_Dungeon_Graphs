using System;
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
        public void ItStoresEdgeNorthProperly()
        {
            var edge = new Hallway();

            _room.EdgeNorth = edge;
            
            Assert.Equal(edge, _room.EdgeNorth);
        }
        
        [Fact]
        public void ItStoresEdgeEastProperly()
        {
            var edge = new Hallway();

            _room.EdgeEast = edge;
            
            Assert.Equal(edge, _room.EdgeEast);
        }
        
        [Fact]
        public void ItStoresEdgeSouthProperly()
        {
            var edge = new Hallway();

            _room.EdgeSouth = edge;
            
            Assert.Equal(edge, _room.EdgeSouth);
        }
        
        [Fact]
        public void ItStoresEdgeWestProperly()
        {
            var edge = new Hallway();

            _room.EdgeWest = edge;
            
            Assert.Equal(edge, _room.EdgeWest);
        }
    }
}
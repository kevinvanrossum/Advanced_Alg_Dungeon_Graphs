using Advanced_Alg_Dungeon_Graphs.Models;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Models
{
    public class PathTest
    {
        private readonly Hallway _hallway;
        
        public PathTest()
        {
            _hallway = new Hallway();
        }
        
        [Fact]
        public void ItStoresVertexACorrectly()
        {
            var room = new Room();

            _hallway.RoomA = room;
            
            Assert.Equal(room, _hallway.RoomA);

        }  
        
        [Fact]
        public void ItStoresVertexBCorrectly()
        {
            var room = new Room();

            _hallway.RoomB = room;
            
            Assert.Equal(room, _hallway.RoomB);

        }

        [Fact]
        public void ItStoresMonsterCorrectly()
        {
            var monster = new Monster();

            _hallway.Monster = monster;
            
            Assert.Equal(monster, _hallway.Monster);
        }
    }
}
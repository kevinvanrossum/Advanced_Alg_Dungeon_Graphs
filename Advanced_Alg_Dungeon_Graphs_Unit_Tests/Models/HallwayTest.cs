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

            _hallway.VertexA = room;
            
            Assert.Equal(room, _hallway.VertexA);

        }  
        
        [Fact]
        public void ItStoresVertexBCorrectly()
        {
            var room = new Room();

            _hallway.VertexB = room;
            
            Assert.Equal(room, _hallway.VertexB);

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
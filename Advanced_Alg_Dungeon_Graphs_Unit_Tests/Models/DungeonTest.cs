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
        public void ItStoresStartRoomProperly()
        {
            var room = new Room();

            _dungeon.StartRoom = room;
            
            Assert.Equal(room, _dungeon.StartRoom);
        }
    }
}
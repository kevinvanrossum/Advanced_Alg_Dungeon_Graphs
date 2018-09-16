using Advanced_Alg_Dungeon_Graphs.Factories;
using Advanced_Alg_Dungeon_Graphs.Models;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Factories
{
    public class RoomFactoryTest
    {
        private readonly IRoomFactory _roomFactory;

        public RoomFactoryTest()
        {
            _roomFactory = new RoomFactory();
        }

        [Fact]
        public void ItCanCreateARoom()
        {
            var room = _roomFactory.Create();
            
            Assert.IsAssignableFrom(typeof(IRoom), room);
            Assert.Equal(room.X, 0);
            Assert.Equal(room.Y, 0);
        }

        [Fact]
        public void ItCanCreateARoomWithCoordinates()
        {
            const int x = 4;
            const int y = 7;
            
            var room = _roomFactory.CreateWithCoordinates(x, y);
            Assert.IsAssignableFrom(typeof(IRoom), room);
            Assert.Equal(room.X, x);
            Assert.Equal(room.Y, y);
        }
    }
}
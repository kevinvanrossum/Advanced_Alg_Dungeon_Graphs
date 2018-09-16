using Advanced_Alg_Dungeon_Graphs.Factories;
using Advanced_Alg_Dungeon_Graphs.Models;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Factories
{
    public class HallwayFactoryTest
    {
        private readonly IHallwayFactory _hallwayFactory;

        public HallwayFactoryTest()
        {
            _hallwayFactory = new HallwayFactory(new MonsterFactory());
        }

        [Fact]
        public void ItCanCreateAHallway()
        {
            var hallway = _hallwayFactory.Create();
            
            Assert.IsAssignableFrom<IHallway>(hallway);
            Assert.IsAssignableFrom<IMonster>(hallway.Monster);
        }

        [Fact]
        public void ItCanCreateAHallwayWithRooms()
        {
            var roomA = new Room();
            var roomB = new Room();
            
            var hallway = _hallwayFactory.CreateWithRooms(roomA, roomB);
            
            Assert.IsAssignableFrom<IHallway>(hallway);
            Assert.IsAssignableFrom<IMonster>(hallway.Monster);
            Assert.IsAssignableFrom<IRoom>(hallway.RoomA);
            Assert.IsAssignableFrom<IRoom>(hallway.RoomB);
            Assert.Equal(roomA, hallway.RoomA);
            Assert.Equal(roomB, hallway.RoomB);
        }
    }
}
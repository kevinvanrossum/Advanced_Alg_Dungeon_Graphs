using Advanced_Alg_Dungeon_Graphs.Factories;
using Advanced_Alg_Dungeon_Graphs.Models;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Factories
{
    public class DungeonFactoryTest
    {
        private readonly DungeonFactory _dungeonFactory;

        public DungeonFactoryTest()
        {
            _dungeonFactory = new DungeonFactory(new RoomFactory(), new HallwayFactory(new MonsterFactory()));
        }

        [Fact]
        public void ItCanCreateADungeon()
        {
            var dungeon = _dungeonFactory.Create();

            Assert.IsAssignableFrom(typeof(IDungeon), dungeon);
            Assert.Equal(dungeon.Rooms.Count, 25);
            Assert.Equal(dungeon.XSize, 5);
            Assert.Equal(dungeon.YSize, 5);
        }

        [Fact]
        public void ItCanCreateADungeonWithXSizeAndYSize()
        {
            const int x = 12;
            const int y = 9;
            var dungeon = _dungeonFactory.CreateWithXSizeAndYSize(x, y);

            Assert.IsAssignableFrom(typeof(IDungeon), dungeon);
            Assert.Equal(dungeon.Rooms.Count, x * y);
            Assert.Equal(dungeon.XSize, x);
            Assert.Equal(dungeon.YSize, y);
        }
    }
}
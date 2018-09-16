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

            Assert.IsAssignableFrom<IDungeon>(dungeon);
            Assert.Equal(25, dungeon.Rooms.Count);
            Assert.Equal(5, dungeon.XSize);
            Assert.Equal(5, dungeon.YSize);
        }

        [Fact]
        public void ItCanCreateADungeonWithXSizeAndYSize()
        {
            const int x = 12;
            const int y = 9;
            var dungeon = _dungeonFactory.CreateWithXSizeAndYSize(x, y);

            Assert.IsAssignableFrom<IDungeon>(dungeon);
            Assert.Equal(x * y, dungeon.Rooms.Count);
            Assert.Equal(x, dungeon.XSize);
            Assert.Equal(y, dungeon.YSize);
        }
    }
}
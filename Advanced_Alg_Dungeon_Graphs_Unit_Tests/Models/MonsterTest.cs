using Advanced_Alg_Dungeon_Graphs.Models;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Models
{
    public class MonsterTest
    {
        private readonly Monster _monster;

        public MonsterTest()
        {
            _monster = new Monster();
        }
        
        [Fact]
        public void ItCanStoreALevelCorrectly()
        {
            const int level = 4;

            _monster.Level = 4;

            Assert.Equal(_monster.Level, level);
        }
        
    }
}
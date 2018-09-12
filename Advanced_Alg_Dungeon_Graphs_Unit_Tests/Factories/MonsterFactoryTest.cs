using Advanced_Alg_Dungeon_Graphs.Factories;
using Advanced_Alg_Dungeon_Graphs.Models;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Factories
{
    public class MonsterFactoryTest
    {
        private readonly IMonsterFactory _monsterFactory;

        public MonsterFactoryTest()
        {
            _monsterFactory = new MonsterFactory();
        }

        [Fact]
        public void ItCanCreateAMonster()
        {
            var monster = _monsterFactory.Create();
            
            Assert.IsAssignableFrom(typeof(IMonster), monster);
        }

        [Fact]
        public void ItCanCreateAMonsterWithRandomLevelInTheRangeZeroAndNine()
        {
            var monster = _monsterFactory.CreateMonsterWithRandomLevel();
            
            Assert.IsAssignableFrom(typeof(IMonster), monster);
            Assert.InRange(monster.Level, 0, 9);
        }
    }
}
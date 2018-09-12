using System;
using Advanced_Alg_Dungeon_Graphs.Models;

namespace Advanced_Alg_Dungeon_Graphs.Factories
{
    public class MonsterFactory : IMonsterFactory
    {
        private Random _randomNumberGenerator;

        public MonsterFactory()
        {
            _randomNumberGenerator = new Random();
        }

        public IMonster Create()
        {
            return CreateMonsterWithRandomLevel();
        }

        public IMonster CreateMonsterWithRandomLevel()
        {
            var randomLevel = _randomNumberGenerator.Next(10);
            var monster = new Monster {Level = randomLevel};
            return monster;
        }
    }
}
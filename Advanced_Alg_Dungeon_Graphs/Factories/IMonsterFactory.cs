using Advanced_Alg_Dungeon_Graphs.Models;

namespace Advanced_Alg_Dungeon_Graphs.Factories
{
    public interface IMonsterFactory
    {
        IMonster Create();
        IMonster CreateMonsterWithRandomLevel();
    }
}
using Advanced_Alg_Dungeon_Graphs.Models;

namespace Advanced_Alg_Dungeon_Graphs.Factories
{
    public interface IDungeonFactory
    {
        IDungeon Create();
        IDungeon CreateWithXSizeAndYSize(int x, int y);
    }
}
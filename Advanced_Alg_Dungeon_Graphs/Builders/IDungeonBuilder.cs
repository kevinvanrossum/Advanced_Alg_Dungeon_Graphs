using Advanced_Alg_Dungeon_Graphs.Models;

namespace Advanced_Alg_Dungeon_Graphs.Builders
{
    public interface IDungeonBuilder
    {
        IDungeon GetDungeon();

        IDungeonBuilder SetSize(int x, int y);
        IDungeonBuilder SetStartingRoom(int x, int y);
        IDungeonBuilder SetEndingRoom(int x, int y);
        IDungeonBuilder SetRandomStartingRoom();
        IDungeonBuilder SetRandomEndingRoom();
    }
}
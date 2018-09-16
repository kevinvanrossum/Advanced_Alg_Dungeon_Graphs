using Advanced_Alg_Dungeon_Graphs.Models;

namespace Advanced_Alg_Dungeon_Graphs.Factories
{
    public interface IRoomFactory
    {
        IRoom Create();
        IRoom CreateWithCoordinates(int x, int y);
    }
}
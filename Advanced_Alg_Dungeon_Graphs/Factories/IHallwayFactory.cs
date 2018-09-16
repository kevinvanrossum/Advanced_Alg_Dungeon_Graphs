using Advanced_Alg_Dungeon_Graphs.Models;

namespace Advanced_Alg_Dungeon_Graphs.Factories
{
    public interface IHallwayFactory
    {
        IHallway Create();
        IHallway CreateWithRooms(IRoom roomA, IRoom roomB);
    }
}
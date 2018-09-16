using System.Collections.Generic;

namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public interface IRoom
    {
        int X { get; }
        int Y { get; }
        

        List<IHallway> AdjacentHallways { get; set; }

        void AddAdjacentHallway(IHallway hallway);
    }
}
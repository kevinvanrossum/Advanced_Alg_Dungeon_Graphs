using System.Collections.Generic;

namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public interface IRoom
    {
        int X { get; }
        int Y { get; }
        bool IsStartRoom { get; set; }
        bool IsEndRoom { get; set; }

        List<IHallway> AdjacentHallways { get; set; }

        void AddAdjacentHallway(IHallway hallway);
    }
}
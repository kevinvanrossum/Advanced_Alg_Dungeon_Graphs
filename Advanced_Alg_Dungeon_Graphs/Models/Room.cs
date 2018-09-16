using System;
using System.Collections.Generic;

namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public class Room : IRoom, IPrintable
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public List<IHallway> AdjacentHallways { get; set; }

        public Room()
        {
            AdjacentHallways = new List<IHallway>();
        }
        
        public void AddAdjacentHallway(IHallway hallway)
        {
            AdjacentHallways.Add(hallway);
        }

        public string ToPrintable()
        {
            return State.NotVisited.ToString();
        }
    }
}
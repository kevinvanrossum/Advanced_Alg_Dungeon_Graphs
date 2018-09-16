using System;
using System.Collections.Generic;

namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public class Room : IRoom, IPrintable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsStartRoom { get; set; }
        public bool IsEndRoom { get; set; }
        
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
            if (IsStartRoom)
            {
                return State.StartingPoint.ToString();
            }

            if (IsEndRoom)
            {
                return State.EndingPoint.ToString();
            }
            return State.NotVisited.ToString();
        }
    }
}
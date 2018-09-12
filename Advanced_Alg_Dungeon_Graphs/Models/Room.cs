namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public class Room : IVertex
    {
        public IEdge EdgeNorth { get; set; }
        public IEdge EdgeEast { get; set; }
        public IEdge EdgeWest { get; set; }
        public IEdge EdgeSouth { get; set; }
    }
}
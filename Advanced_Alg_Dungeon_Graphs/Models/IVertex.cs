namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public interface IVertex
    {
        IEdge EdgeNorth { get; set; }
        IEdge EdgeEast { get; set; }
        IEdge EdgeWest { get; set; }
        IEdge EdgeSouth { get; set; }

    }
}
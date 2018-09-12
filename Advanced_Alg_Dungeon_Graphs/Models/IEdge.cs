namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public interface IEdge
    {
        IVertex VertexA { get; set; }
        IVertex VertexB { get; set; }
    }
}
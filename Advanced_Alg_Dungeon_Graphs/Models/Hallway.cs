namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public class Hallway : IEdge
    {
        public IVertex VertexA { get; set; }
        public IVertex VertexB { get; set; }
        public IMonster Monster { get; set; }
    }
}
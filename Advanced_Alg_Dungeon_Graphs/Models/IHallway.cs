namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public interface IHallway
    {
        IRoom RoomA { get; set; }
        IRoom RoomB { get; set; }
        IMonster Monster { get; set; }
        void Collapse();

    }
}
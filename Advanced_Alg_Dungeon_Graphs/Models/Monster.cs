namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public class Monster : IMonster
    {
        public int Level { get; set; }
        
        public void TrainInHyperBolicTimeChamber()
        {
            Level = 9;
        }
    }
}
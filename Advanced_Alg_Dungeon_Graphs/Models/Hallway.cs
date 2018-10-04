﻿using Advanced_Alg_Dungeon_Graphs.Factories;

namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public class Hallway : IHallway, IPrintable
    {
        public IRoom RoomA { get; set; }
        public IRoom RoomB { get; set; }
        public IMonster Monster { get; set; }
        public bool Collapsed { get; set; }
        
        
        public void Collapse()
        {
            Collapsed = true;
        }

        public Hallway()
        {
            var monsterFactory = new MonsterFactory();
            Monster = monsterFactory.CreateMonsterWithRandomLevel();
            Collapsed = false;
        }
        
        

        public string ToPrintable()
        {
            if (Collapsed)
            {
                return "~";
            }
            return Monster.Level.ToString();
        }
    }
}
using System;
using Advanced_Alg_Dungeon_Graphs.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public class Hallway : IHallway, IPrintable
    {
        public IRoom RoomA { get; set; }
        public IRoom RoomB { get; set; }
        public IMonster Monster { get; set; }

        public Hallway()
        {
            var monsterFactory = new MonsterFactory();
            Monster = monsterFactory.CreateMonsterWithRandomLevel();
        }

        public string ToPrintable()
        {
            return Monster.Level.ToString();
        }
    }
}
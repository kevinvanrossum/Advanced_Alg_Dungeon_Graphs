﻿using System.Collections.Generic;

namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public interface IDungeon
    {
        int XSize { get; set; }
        int YSize { get; set; }
        
        IRoom StartRoom { get; set; }

        IRoom EndRoom { get; set; }

        List<IRoom> Rooms { get; set; }

        List<IHallway> Hallways { get; set; }

        void AddRoom(IRoom room);

        void AddHallway(IHallway hallway);
        void ActivateGrenade();

        List<IRoom> ActivateCompass();

        IRoom GetRoom(int x, int y);
        
        int ActivateTalisman();
        void Restore();
        void ActivateCheat();
        void ActivateRandomizer();
    }
}
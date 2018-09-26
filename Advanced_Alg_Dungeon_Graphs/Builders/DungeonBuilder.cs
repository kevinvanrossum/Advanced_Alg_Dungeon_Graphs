using System;
using Advanced_Alg_Dungeon_Graphs.Factories;
using Advanced_Alg_Dungeon_Graphs.Models;

namespace Advanced_Alg_Dungeon_Graphs.Builders
{
    public class DungeonBuilder : IDungeonBuilder
    {
        private IDungeon _dungeon;
        private readonly IDungeonFactory _dungeonFactory;
        private readonly Random _randomNumberGenerator;


        public DungeonBuilder(IDungeonFactory iDungeonFactory)
        {
            _dungeonFactory = iDungeonFactory;
            _dungeon = iDungeonFactory.Create();
            _randomNumberGenerator = new Random();
        }


        public IDungeon GetDungeon()
        {
            return _dungeon;
        }

        public IDungeonBuilder SetSize(int x, int y)
        {
            _dungeon = _dungeonFactory.CreateWithXSizeAndYSize(x, y);
            return this;
        }

        public IDungeonBuilder SetStartingRoom(int x, int y)
        {
            _dungeon.StartRoom = _dungeon.GetRoom(x, y);
            _dungeon.StartRoom.IsStartRoom = true;

            return this;
        }

        public IDungeonBuilder SetEndingRoom(int x, int y)
        {
            _dungeon.EndRoom = _dungeon.GetRoom(x, y);
            _dungeon.EndRoom.IsEndRoom = true;

            return this;
        }

        public IDungeonBuilder SetRandomStartingRoom()
        {
            var x = _randomNumberGenerator.Next(0, _dungeon.XSize - 1);
            var y = _randomNumberGenerator.Next(0, _dungeon.YSize - 1);
            
            if (x == _dungeon?.StartRoom?.X && y == _dungeon?.StartRoom?.Y)
            {
                return SetRandomStartingRoom();
            }
            
            SetStartingRoom(x, y);
            return this;
        }

        public IDungeonBuilder SetRandomEndingRoom()
        {
            // Hoeft niet -1?
            var x = _randomNumberGenerator.Next(0, _dungeon.XSize - 1);
            var y = _randomNumberGenerator.Next(0, _dungeon.YSize - 1);

            if (x == _dungeon?.StartRoom?.X && y == _dungeon?.StartRoom?.Y)
            {
                return SetRandomEndingRoom();
            }
            
            SetEndingRoom(x, y);
            return this;
        }
    }
}

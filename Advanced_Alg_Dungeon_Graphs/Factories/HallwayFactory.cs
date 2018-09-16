using Advanced_Alg_Dungeon_Graphs.Models;

namespace Advanced_Alg_Dungeon_Graphs.Factories
{
    public class HallwayFactory : IHallwayFactory
    {
        private readonly IMonsterFactory _iMonsterFactory;

        public HallwayFactory(IMonsterFactory iMonsterFactory)
        {
            _iMonsterFactory = iMonsterFactory;
        }

        public IHallway Create()
        {
            return new Hallway()
            {
                Monster = _iMonsterFactory.CreateMonsterWithRandomLevel()
            };
        }

        public IHallway CreateWithRooms(IRoom roomA, IRoom roomB)
        {
            var hallway = Create();
            hallway.RoomA = roomA;
            hallway.RoomB = roomB;
            return hallway;
        }
    }
}
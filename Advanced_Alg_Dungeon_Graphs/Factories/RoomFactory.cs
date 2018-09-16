using Advanced_Alg_Dungeon_Graphs.Models;

namespace Advanced_Alg_Dungeon_Graphs.Factories
{
    public class RoomFactory : IRoomFactory
    {
        public IRoom Create()
        {
            return new Room() {X = 0, Y = 0};
        }

        public IRoom CreateWithCoordinates(int x, int y)
        {
            return new Room() {X = x, Y = y};
        }
    }
}
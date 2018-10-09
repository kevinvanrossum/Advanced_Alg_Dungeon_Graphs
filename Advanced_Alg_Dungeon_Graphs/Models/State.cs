namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public static class State
    {
        public static char StartingPoint => 'S';
        public static char EndingPoint => 'E';
        public static char NotVisited => 'X';
        public static char Visited => '*';
        public static char ShortestPath => '#';
        public static char Collapsed => '~';
        public static char Horizontal => '|';
        public static char Veritcal => '-';
        public static char Monster => '0';
    }
}
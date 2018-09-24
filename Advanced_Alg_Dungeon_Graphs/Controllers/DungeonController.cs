using System;
using System.Collections.Generic;
using Advanced_Alg_Dungeon_Graphs.Builders;
using Advanced_Alg_Dungeon_Graphs.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced_Alg_Dungeon_Graphs.Controllers
{
    public class DungeonController
    {
        private readonly ServiceProvider _serviceProvider;
        private IDungeon _dungeon;

        public DungeonController(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Explore()
        {
            var builder = _serviceProvider.GetService<IDungeonBuilder>();

            Console.WriteLine("How many rooms vertical?");
            var xSize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many rooms horizontal?");
            var ySize = Convert.ToInt32(Console.ReadLine());
            builder.SetSize(xSize, ySize);

            Console.WriteLine("Do you want random starting and ending rooms [Y|N]");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
              builder
                  .SetRandomStartingRoom()
                  .SetRandomEndingRoom();
            }
            else
            {
              Console.WriteLine("What is the X of the starting room?");
              var xStart = Convert.ToInt32(Console.ReadLine());
              Console.WriteLine("What is the Y of the starting room?");
              var yStart = Convert.ToInt32(Console.ReadLine());
              builder.SetStartingRoom(xStart, yStart);

              Console.WriteLine("What is the X of the ending room?");
              var xEnd = Convert.ToInt32(Console.ReadLine());
              Console.WriteLine("What is the Y of the ending room?");
              var yEnd = Convert.ToInt32(Console.ReadLine());
              builder.SetEndingRoom(xEnd, yEnd);
            }

            _dungeon = builder.GetDungeon();

            Console.Clear();

            PrintHelpText();
            PrintDungeon();
            Console.WriteLine("Acties: talisman, handgranaat, kompas");

            while (HandleAction() != false)
            {
                Console.ReadLine();
            }
            Console.WriteLine("The end");
            Console.ReadLine();
            
        }

        private bool HandleAction()
        {
            switch (Console.ReadLine())
            {
                case "talisman": Console.WriteLine("The ending room is {0} rooms away.", ActivateTalisman()); return true;
                case "handgranaat": break;
                case "kompas": break;
                /* case noord
                  * case oost
                  * case zuid
                  * case west
                  */                 
            }
            return false;
        }

        public int ActivateTalisman() // breadth first search
        {
            if (_dungeon == null || _dungeon.StartRoom == null || _dungeon.EndRoom == null) return -1;
            
            Dictionary<IRoom, IRoom> roomMemory = new Dictionary<IRoom, IRoom>();
            Queue<IRoom> queue = new Queue<IRoom>();
      
            queue.Enqueue(_dungeon.StartRoom);
            IRoom current = null;

            while (queue.Count > 0 && current != _dungeon.EndRoom)
            {
                current = queue.Dequeue();
                if (current == null) continue;
                foreach (IHallway hallway in current.AdjacentHallways)
                {
                    IRoom roomAtOtherSide = (hallway.RoomA == current) ? hallway.RoomB : hallway.RoomA;
                    if (roomMemory.ContainsKey(roomAtOtherSide)) continue;
                    roomMemory[roomAtOtherSide] = current;
                    queue.Enqueue(roomAtOtherSide);
                }
            }

            return GetShortestPath(roomMemory);
        }

        private int GetShortestPath(Dictionary<IRoom, IRoom> memory)
        {
            int count = 0;
            var current = _dungeon.EndRoom;
            while (!current.Equals(_dungeon.StartRoom))
            {
                count++;
                current = memory[current];
            }
            return count;
        }
        private void PrintDungeon()
        {
            var printable = ((IPrintable) _dungeon).ToPrintable();

            PrintWithMarkup(printable);
        }

        private static void PrintWithMarkup(string printable)
        {
            foreach (var c in printable)
            {
                if (c == 'E' || c == 'S')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(c);
                }
                else
                {
                    Console.ResetColor();
                    Console.Write(c);
                }
            }
        }

        private static void PrintHelpText()
        {
            PrintWithMarkup("S = Room: startpunt");
            Console.WriteLine();
            PrintWithMarkup("E = Room: eindpunt");
            Console.WriteLine();
            PrintWithMarkup("X = Room: niet bezocht");
            Console.WriteLine();
            PrintWithMarkup("* = Room: bezocht");
            Console.WriteLine();
            PrintWithMarkup("~ = Hallway: ingestort");
            Console.WriteLine();
            PrintWithMarkup("0 = Hallway: level tegenstander (cost)");
            Console.WriteLine(Environment.NewLine);
        }

#region Testing Purpose - Should be moved
        // Creates a default 10x10 dungeon for usage in the tests. Should probably be moved somewhere.
        public IDungeon CreateTestDungeon()
        {
            var builder = _serviceProvider.GetService<IDungeonBuilder>();
            builder.SetSize(10,10);
            builder.SetStartingRoom(0, 0);
            builder.SetEndingRoom(4, 4);
            _dungeon = builder.GetDungeon();
            return _dungeon;
        }
#endregion
    }
}

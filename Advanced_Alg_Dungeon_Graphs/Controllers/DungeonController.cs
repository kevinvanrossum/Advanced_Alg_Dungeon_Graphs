using System;
using System.Collections.Generic;
using System.Linq;
using Advanced_Alg_Dungeon_Graphs.Builders;
using Advanced_Alg_Dungeon_Graphs.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced_Alg_Dungeon_Graphs.Controllers
{
    public class DungeonController
    {
        private readonly ServiceProvider _serviceProvider;
        private IDungeon _dungeon;
        private bool _playing;

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


            PlayGame();


            Console.WriteLine("The end");
        }

        private void PlayGame()
        {
            _playing = true;


            while (_playing)
            {            
                Console.Clear();

                PrintHelpText();
                PrintDungeon();

                HandleAction();
            }
        }

        private void HandleAction()
        {
            string command = null;
            while (command == null || command.Equals(""))
            {
                Console.WriteLine("Acties: talisman, handgranaat, kompas, exit");
                command = Console.ReadLine()?.ToLower();
            }

            if (command[0].Equals('t'))
            {
                var steps = ActivateTalisman();
                var stepWord = steps > 1 ? "stappen" : "stap";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"=> De talisman licht op en fluistert dat het eindpunt {steps} {stepWord} ver weg is.");
                Console.ResetColor();
            }

            if (command[0].Equals('h'))
            {
                ActivateGrenade();
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=> De kerker schudt op zijn grondvesten, de tegenstander in een aangrenzende hallway is vermorzeld! Een donderend geluid maakt duidelijk dat gedeeltes van de kerker zijn ingestort...");
                Console.ResetColor();
            }

            if (command[0].Equals('k'))
            {
                _dungeon.ActivateCompass();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Je haalt het kompas uit je zak. Het trilt in je hand en projecteert in lichtgevende letters op de muur:");
                // Hieronder moet nog dynamisch worden
                Console.WriteLine("Noord – Noord – Oost – Oost – Noord – Noord – West");
                Console.WriteLine("4 tegenstanders (level 2, level 1, level 3, level 1)");
                Console.ResetColor();
            }

            if (command[0].Equals('m'))
            {
                PrintDungeon();
            }

            if (command[0].Equals('e'))
            {
                _playing = false;
            }

            if (command[0].Equals('c'))
            {
                // change this to only in dijkstra shortest path
                _dungeon.Hallways.ForEach(h => h.Monster.TrainInHyperBolicTimeChamber());
            }

            Console.WriteLine();
            Console.WriteLine("Druk op een knop om verder te gaan");
            Console.ReadLine();
        }

        public int ActivateTalisman()
        {
            return _dungeon.Talisman();
        }

        /// <summary>
        /// MST by Kruskal
        /// </summary>
        /// <returns>The amount of destroyed hallways.</returns>
        public int ActivateGrenade()
        {
            var hallways = _dungeon.Hallways;
            hallways = hallways.OrderBy(i => i.Monster.Level).ToList();

            var rooms = new List<IRoom>();
            var mst = new List<IHallway>();


            foreach (var hallway in hallways)
            {
                // Either or both rooms are not yet in the mst
                if (!rooms.Contains(hallway.RoomA) || !rooms.Contains(hallway.RoomB))
                {
                    mst.Add(hallway);
                    if (!rooms.Contains(hallway.RoomA)) rooms.Add(hallway.RoomA);
                    if (!rooms.Contains(hallway.RoomB)) rooms.Add(hallway.RoomB);
                }

                // Both rooms are already in the mst, but may not be connected yet
                else if (rooms.Contains(hallway.RoomA) && rooms.Contains(hallway.RoomB))
                {
                    // continue or hallway connects two parts of mst
                    // check if hallway.RoomA is in a different tree than hallway.RoomB
                    // Find RoomB from RoomA using all hallways in hallways
                    // If it cant find RoomB it must be in a different tree, so add it.
                    if (!FindRoom(hallway.RoomA, hallway.RoomB, mst)) mst.Add(hallway);
                }

                hallway.Collapse();
            }

            return mst.Count;
        }

        /// <summary>
        /// Try to find the endRoom from the startRoom using the current minimum spanning tree
        /// </summary>
        /// <param name="startRoom">Room from where one starts searching</param>
        /// <param name="endRoom">Room one wants to find</param>
        /// <param name="currentMst">Minimum spanning tree at current point in time</param>
        /// <returns>True if the room can be found, false if not.</returns>
        private bool FindRoom(IRoom startRoom, IRoom endRoom, ICollection<IHallway> currentMst)
        {
            var current = startRoom;
            var memory = new List<IHallway>();
            foreach (var unused in currentMst)
            {
                // foreach adjacent hallway of currentRoom that is also in currentMST
                foreach (var hallway in current.AdjacentHallways)
                {
                    if (!currentMst.Contains(hallway) || memory.Contains(hallway)) continue;
                    if (hallway.RoomA == endRoom || hallway.RoomB == endRoom) return true;

                    current = hallway.RoomA == current ? hallway.RoomB : hallway.RoomA;
                    memory.Add(hallway);
                }
            }

            return false;
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
                switch (c)
                {
                    case 'E':
                    case 'S':
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(c);
                        break;
                    case '~':
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(c);
                        break;
                    default:
                        Console.ResetColor();
                        Console.Write(c);
                        break;
                }
            }
        }

        public IDungeon GetDungeon()
        {
            return _dungeon;
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
            PrintWithMarkup("# = Room: kortste pad");
            Console.WriteLine();
            PrintWithMarkup("~ = Hallway: ingestort");
            Console.WriteLine();
            PrintWithMarkup("0 = Hallway: level tegenstander (cost)");
            Console.WriteLine(Environment.NewLine);
        }

        #region Testing Purpose - Should be moved

        /// <summary>
        /// Creates a default dungeon with StartRoom(4,4) and EndRoom(0,0)
        /// </summary>
        /// <param name="x">Width of dungeon</param>
        /// <param name="y">Height of dungeon</param>
        /// <returns>A new dungeon.</returns>
        public IDungeon CreateTestDungeon(int x, int y, bool random)
        {
            var builder = _serviceProvider.GetService<IDungeonBuilder>();
            builder.SetSize(x, y);
            if (random)
            {
                builder.SetRandomStartingRoom();
                builder.SetRandomEndingRoom();
            }
            else
            {
                builder.SetStartingRoom(4, 4);
                builder.SetEndingRoom(0, 0);
            }

            _dungeon = builder.GetDungeon();
            return _dungeon;
        }

        #endregion
    }
}
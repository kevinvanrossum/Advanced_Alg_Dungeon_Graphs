using System;
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
            Console.ReadLine();
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
    }
}
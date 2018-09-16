using System;
using Advanced_Alg_Dungeon_Graphs.Factories;
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
            Console.WriteLine("What is the X size?");
            var x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What is the Y size?");
            var y = Convert.ToInt32(Console.ReadLine());
            
            _dungeon = _serviceProvider.GetService<IDungeonFactory>().CreateWithXSizeAndYSize(x, y);
            Console.WriteLine(((IPrintable) _dungeon).ToPrintable());
            
            Console.ReadLine();
        }
    }
}
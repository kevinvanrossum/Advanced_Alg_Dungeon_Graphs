using Advanced_Alg_Dungeon_Graphs.Builders;
using Advanced_Alg_Dungeon_Graphs.Controllers;
using Advanced_Alg_Dungeon_Graphs.Factories;
using Advanced_Alg_Dungeon_Graphs.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Algorithms
{
  public class BreadthFirstSearchTest
  {
    private readonly DungeonController _controller;
    private readonly IDungeon _dungeon;
    public BreadthFirstSearchTest()
    {
      var serviceProvider = new ServiceCollection()
                  .AddSingleton<IMonsterFactory, MonsterFactory>()
                  .AddSingleton<IHallwayFactory, HallwayFactory>()
                  .AddSingleton<IRoomFactory, RoomFactory>()
                  .AddSingleton<IDungeonFactory, DungeonFactory>()
                  .AddSingleton<IDungeonBuilder, DungeonBuilder>()
                  .BuildServiceProvider();

      _controller = new DungeonController(serviceProvider);
    }

    [Fact]
    public void ItCanSearchBreadthFirst()
    {
      _controller.CreateTestDungeon(5, 5, false);
      var value = _controller.ActivateTalisman();
      Assert.Equal(8, value);
    }

    [Fact]
    public void ItDidNotFail()
    {
      _controller.CreateTestDungeon(100, 100, true);
      var value = _controller.ActivateTalisman();
      Assert.NotStrictEqual(-1, value);
    }
  }
}

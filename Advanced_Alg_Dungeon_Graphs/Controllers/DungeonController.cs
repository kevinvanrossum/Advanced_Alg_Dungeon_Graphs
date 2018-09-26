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
        case "handgranaat": ActivateGrenade();  break;
        case "kompas": break;
        case "map": PrintDungeon(); break;
          /* case noord
            * case oost
            * case zuid
            * case west
            */
      }
      return false;
    }

    /// <summary>
    /// Search Breadth-First through the dungeon from the StartRoom to the EndRoom.
    /// After memorizing from which Room each Room is reached, call GetShortestPath()
    /// to determine the length of the shortest path.
    /// </summary>
    /// <returns>The amount of steps needed to reach the EndRoom from StartRoom</returns>
    public int ActivateTalisman()
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

    /// <summary>
    /// Determine the shortest path based on memory.
    /// </summary>
    /// <param name="memory">Dictionary containing the Room from which some Room was reached.</param>
    /// <returns>The amount of steps needed to reach EndRoom from StartRoom.</returns>
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

    /// <summary>
    /// MST by Kruskal
    /// </summary>
    /// <returns>The amount of destroyed hallways.</returns>
    public int ActivateGrenade()
    {
      List<IHallway> hallways = _dungeon.Hallways;
      hallways = hallways.OrderBy(i => i.Monster.Level).ToList();

      List<IRoom> rooms = new List<IRoom>();
      List<IHallway> mst = new List<IHallway>();
      
      
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


      }
      return mst.Count;
    }

    /// <summary>
    /// Try to find the endRoom from the startRoom using the current minimum spanning tree
    /// </summary>
    /// <param name="startRoom">Room from where one starts searching</param>
    /// <param name="endRoom">Room one wants to find</param>
    /// <param name="currentMST">Minimum spanning tree at current point in time</param>
    /// <returns>True if the room can be found, false if not.</returns>
    private bool FindRoom(IRoom startRoom, IRoom endRoom, List<IHallway> currentMST)
    {
      IRoom current = startRoom;
      List<IHallway> memory = new List<IHallway>();
      for (int i = 0; i < currentMST.Count; i++)
      {
        // foreach adjacent hallway of currentRoom that is also in currentMST
        foreach (var hallway in current.AdjacentHallways)
        {
          if (currentMST.Contains(hallway) && !memory.Contains(hallway)) // Current hallway is in MST and not in memory
          {
            if (hallway.RoomA == endRoom || hallway.RoomB == endRoom) return true;

            if (hallway.RoomA == current) current = hallway.RoomB;
            else current = hallway.RoomA;
            memory.Add(hallway);
          }
        }

      }
      return false;
    }

    private void PrintDungeon()
    {
      var printable = ((IPrintable)_dungeon).ToPrintable();

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
            builder.SetSize(x,y);
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

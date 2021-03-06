﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced_Alg_Dungeon_Graphs.Models
{
    public class Dungeon : IDungeon, IPrintable
    {
        public int XSize { get; set; }
        public int YSize { get; set; }
        public IRoom StartRoom { get; set; }
        public IRoom EndRoom { get; set; }
        public List<IRoom> Rooms { get; set; }
        public List<IHallway> Hallways { get; set; }

        public Dungeon()
        {
            Rooms = new List<IRoom>();
            Hallways = new List<IHallway>();
        }

        public Dungeon(int xSize, int ySize) : this()
        {
            XSize = xSize;
            YSize = ySize;
        }

        public Dungeon(IRoom startRoom, IRoom endRoom, int xSize, int ySize) : this(xSize, ySize)
        {
            StartRoom = startRoom;
            EndRoom = endRoom;
        }

        public void AddRoom(IRoom room)
        {
            Rooms.Add(room);
        }

        public void AddHallway(IHallway hallway)
        {
            Hallways.Add(hallway);
        }

        private void ClearShortestPath()
        {
            foreach (var room in Rooms) room.IsShortestPath = false;
        }

        public IRoom GetRoom(int x, int y)
        {
            return Rooms.FirstOrDefault(room => room.X == x && room.Y == y);
        }

        /// <summary>
        /// Search Breadth-First through the dungeon from the StartRoom to the EndRoom.
        /// After memorizing from which Room each Room is reached, call GetShortestPath()
        /// to determine the length of the shortest path.
        /// </summary>
        /// <returns>The amount of steps needed to reach the EndRoom from StartRoom</returns>
        public int ActivateTalisman()
        {
            if (StartRoom == null || EndRoom == null) return -1;

            var roomMemory = new Dictionary<IRoom, IRoom>();
            var queue = new Queue<IRoom>();
            queue.Enqueue(StartRoom);
            IRoom current = null;

            while (queue.Count > 0 && current != EndRoom)
            {
                current = queue.Dequeue();
                if (current == null) continue;
                foreach (var hallway in current.AdjacentHallways)
                {
                    if (hallway.Collapsed) continue;
                    var roomAtOtherSide = hallway.RoomA == current ? hallway.RoomB : hallway.RoomA;
                    if (roomMemory.ContainsKey(roomAtOtherSide)) continue;
                    roomMemory[roomAtOtherSide] = current;
                    queue.Enqueue(roomAtOtherSide);
                }
            }

            return GetShortestPath(roomMemory);
        }

        public void Restore()
        {
            Hallways.ForEach(h => h.Restore());
            ClearShortestPath();
        }

        /// <summary>
        /// Determine the shortest path based on memory.
        /// </summary>
        /// <param name="memory">Dictionary containing the Room from which some Room was reached.</param>
        /// <returns>The amount of steps needed to reach EndRoom from StartRoom.</returns>
        private int GetShortestPath(Dictionary<IRoom, IRoom> memory)
        {
            var count = 0;
            var current = EndRoom;
            while (!current.Equals(StartRoom))
            {
                count++;
                current = memory[current];
            }

            return count;
        }

        /// <summary>
        /// Adds an item to the list if the item is not in the list.
        /// </summary>
        private void TryAdd<T>(List<T> list, T item)
        {
            if (!list.Contains(item)) list.Add(item);
        }

        /// <summary>
        /// Adds all items to the list that are not in the list
        /// </summary>
        private void TryAddRange<T>(List<T> list, List<T> addableItems)
        {
            foreach (var add in addableItems)
            {
                if (!list.Contains(add)) list.Add(add);
            }
        }

        public void ActivateGrenade()
        {
            ClearShortestPath();

            List<IHallway> mst = new List<IHallway>();
            List<IRoom> mstRooms = new List<IRoom>();
            List<IHallway> possible_hallways = new List<IHallway>();

            // Set random adjacent hallway to level 0
            Random random = new Random();
            StartRoom.AdjacentHallways[random.Next(1, StartRoom.AdjacentHallways.Count)].Monster.Level = 0;

            mstRooms.Add(StartRoom);
            possible_hallways.AddRange(StartRoom.AdjacentHallways);

            do
            {
                var sortedList = possible_hallways.OrderBy(o => o.Monster.Level).ToList();
                var toAdd = sortedList.First();

                if (!mstRooms.Contains(toAdd.RoomA) || !mstRooms.Contains(toAdd.RoomB))
                {
                    TryAdd(mstRooms, toAdd.RoomA);
                    TryAdd(mstRooms, toAdd.RoomB);
                    TryAddRange(possible_hallways, toAdd.RoomA.AdjacentHallways);
                    TryAddRange(possible_hallways, toAdd.RoomB.AdjacentHallways);
                    mst.Add(toAdd);
                }
                possible_hallways.Remove(toAdd);
            } while (mst.Count < Rooms.Count - 1 && mstRooms.Count < Rooms.Count);

            foreach (var hallway in Hallways) if (!mst.Contains(hallway)) hallway.Collapse();
        }

        public List<IRoom> ActivateCompass()
        {
            ClearShortestPath();

            var previous = new Dictionary<IRoom, IRoom>();
            var distances = new Dictionary<IRoom, int>();
            var rooms = new List<IRoom>();

            List<IRoom> path = null;

            foreach (var room in Rooms)
            {
                if (room == StartRoom) distances[room] = 0;
                else distances[room] = int.MaxValue;
                rooms.Add(room);
            }

            while (rooms.Count != 0)
            {
                rooms.Sort((x, y) => distances[x] - distances[y]);

                var smallest = rooms[0];
                rooms.Remove(smallest);

                if (smallest == EndRoom)
                {
                    path = new List<IRoom>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }
                    break;
                }

                if (distances[smallest] == int.MaxValue) break;

                foreach(var neighbor in smallest.AdjacentHallways)
                {
                    if (neighbor.Collapsed) continue;

                    if (smallest == neighbor.RoomA)
                    {
                        var alt = distances[smallest] + neighbor.Monster.Level;
                        if (alt < distances[neighbor.RoomB])
                        {
                            distances[neighbor.RoomB] = alt;
                            previous[neighbor.RoomB] = smallest;
                        }
                    }
                    else if (smallest == neighbor.RoomB)
                    {
                        var alt = distances[smallest] + neighbor.Monster.Level;
                        if (alt < distances[neighbor.RoomA])
                        {
                            distances[neighbor.RoomA] = alt;
                            previous[neighbor.RoomA] = smallest;
                        }
                    }
                }
                
            }
            return path;
        }

        public void ActivateCheat()
        {
            ClearShortestPath();
            Hallways.ForEach(h => h.Monster.TrainInHyperBolicTimeChamber());
        }

        public void ActivateRandomizer()
        {
            ClearShortestPath();
            var randomLevel = new Random();
            Hallways
                .Where(h => !h.Collapsed)
                .ToList()
                .ForEach(h => h.Monster.Level = randomLevel.Next(0, 9));
        }

        public string ToPrintable()
        {
            var result = "";
            for (var y = 0; y < YSize; y++)
            {
                for (var x = 0; x < XSize; x++)
                {
                    var currentRoom = (Room)GetRoom(x, y);
                    result += $"{currentRoom.ToPrintable()}";
                    if (currentRoom.X >= XSize - 1) continue;
                    var hallway = (Hallway)currentRoom.AdjacentHallways.First(h => h.RoomA == currentRoom);
                    result += $" {hallway.ToPrintable()} ";
                }

                result += Environment.NewLine;

                for (var x = 0; x < XSize; x++)
                {
                    var currentRoom = (Room)GetRoom(x, y);
                    if (currentRoom.Y >= YSize - 1) continue;
                    var hallway = (Hallway)currentRoom.AdjacentHallways.Last(h => h.RoomA == currentRoom);
                    result += $"{hallway.ToPrintable()}   ";
                }

                result += Environment.NewLine;
            }

            return result;
        }
    }

    
}
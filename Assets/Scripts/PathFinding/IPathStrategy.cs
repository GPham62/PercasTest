using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace PathFinding
{
    public interface IPathStrategy
    {
        List<Vector2Int> FindPath(Vector2Int agentPos, Vector2Int goalPos, Grid<TileGrid> grid);
    }

    public class AStar : IPathStrategy
    {
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1,0), new Vector2Int(-1,0),
            new Vector2Int(0,1), new Vector2Int(0,-1)
        };
    
        public List<Vector2Int> FindPath(Vector2Int agentPos, Vector2Int goalPos, Grid<TileGrid> grid)
        {
            return null;
        }
    }

    public class BFS : IPathStrategy
    {
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1,0), new Vector2Int(-1,0),
            new Vector2Int(0,1), new Vector2Int(0,-1)
        };
    
        public List<Vector2Int> FindPath(Vector2Int agentPos, Vector2Int goalPos, Grid<TileGrid> grid)
        {
            Queue<Vector2Int> queue = new();
            HashSet<Vector2Int> visited = new();
            Dictionary<Vector2Int, Vector2Int> cameFrom = new();
            queue.Enqueue(agentPos);
            visited.Add(agentPos);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var dir in directions)
                {
                    var nextPos = current + dir;
                    if (!grid.IsValidPosition(nextPos))
                        continue;
                    if (nextPos == goalPos)
                    {
                        List<Vector2Int> path = new(){ current };
                        while (cameFrom.ContainsKey(current))
                        {
                            current = cameFrom[current];
                            path.Add(current);
                        }
                        path.Remove(agentPos);
                        return path;
                    }
                    if (!visited.Contains(nextPos) && nextPos != goalPos)
                    {
                        queue.Enqueue(nextPos);
                        visited.Add(nextPos);
                        cameFrom[nextPos] = current;
                    }
                }
            }
            return null;
        }
    }
}
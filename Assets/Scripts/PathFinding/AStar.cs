using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace PathFinding
{
    public class AStar : IPathStrategy
    {
        private const int VERTICAL_COST = 10;
        private const int DIAGONAL_COST = 14;
        
        public List<Vector2Int> FindPath(Vector2Int agentPos, Vector2Int goalPos, Grid<TileGrid> grid)
        {
            var openSet = new List<Vector2Int> { agentPos };
            var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
            var gScore = new Dictionary<Vector2Int, int> { [agentPos] = 0 };

            while (openSet.Count > 0)
            {
                openSet.Sort((a, b) =>
                    (gScore[a] + Heuristic(a, goalPos)).CompareTo(gScore[b] + Heuristic(b, goalPos))
                );
                var current = openSet[0];
                openSet.RemoveAt(0);

                if (current == goalPos)
                {
                    List<Vector2Int> path = new List<Vector2Int>();
                    while (cameFrom.ContainsKey(current))
                    {
                        current = cameFrom[current];
                        path.Add(current);
                    }
                    path.Remove(agentPos);
                    return path;
                }

                foreach (var dir in GameplayConstant.directions)
                {
                    var next = current + dir;
                    if (!grid.IsValidPosition(next.x, next.y)) continue;

                    var tile = grid.GetValue(next.x, next.y).GetTile();
                    if (!tile.IsWalkable()) continue;
                    bool isDiagonal = dir.x != 0 && dir.y != 0;
                    int score = gScore[current] + (isDiagonal ? DIAGONAL_COST : VERTICAL_COST);

                    if (!gScore.ContainsKey(next) || score < gScore[next])
                    {
                        cameFrom[next] = current;
                        gScore[next] = score;

                        if (!openSet.Contains(next))
                            openSet.Add(next);
                    }
                }
            }

            return new();
        }

        private int Heuristic(Vector2Int a, Vector2Int b)
        {
            var dx = Mathf.Abs(a.x - b.x);
            var dy = Mathf.Abs(a.y - b.y);
            return VERTICAL_COST * (dx + dy) + (DIAGONAL_COST - 2 * VERTICAL_COST) * Mathf.Min(dx, dy);
        }
    }
}
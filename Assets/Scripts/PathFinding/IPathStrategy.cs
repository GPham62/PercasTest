using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace PathFinding
{
    public interface IPathStrategy
    {
        List<Vector2Int> FindPath(Vector2Int agentPos, Vector2Int goalPos, Grid<TileGrid> grid);
    }
}
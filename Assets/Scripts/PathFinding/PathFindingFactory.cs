using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public static class PathFindingFactory
{
    public static IPathStrategy GetPathFinding(SearchType searchType)
    {
        return searchType switch
        {
            SearchType.BFS => new BFS(),
            SearchType.Astar => new AStar()
        };
    }
}
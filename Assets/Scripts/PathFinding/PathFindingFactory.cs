using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public static class PathFindingFactory
{
    public static IPathStrategy GetPathFinding(MapMaker.SearchType searchType)
    {
        return searchType switch
        {
            MapMaker.SearchType.BFS => new BFS()
        };
    }
}
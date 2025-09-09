using System;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapMaker : MonoBehaviour
{
    [SerializeField] private int mapWidth = 10;
    [SerializeField] private int mapHeight = 10;
    [SerializeField] private float cellSize;
    [SerializeField] private TileEmpty tileEmpty;
    [SerializeField] private TileGoal tileGoal;
    [SerializeField] private TileWall tileWall;
    [SerializeField] private TileNPC tileNPC;
    [SerializeField] private GameObject gridHolder;
    [SerializeField] private int minDistance = 5;
    [SerializeField] private SearchType searchType = SearchType.Astar;

    private Grid<TileGrid> _grid;

    private void Awake()
    {
        _grid = new Grid<TileGrid>(mapWidth, mapHeight, cellSize, (grid, x, y) => new TileGrid(grid, x, y));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (Transform child in gridHolder.transform)
            {
                Destroy(child.gameObject);
            }
            (var agentPos, var goalPos) = PickAgentAndGoalPositions();
            DrawMap(agentPos, goalPos);
            var traversal = PathFindingFactory.GetPathFinding(searchType).FindPath(agentPos, goalPos, _grid);
            foreach (var gridPos in traversal)
            {
                _grid.GetValue(gridPos).GetTile().ChangeColor(Color.yellow);
            }
        }
    }

    private (Vector2Int, Vector2Int) PickAgentAndGoalPositions()
    {
        Vector2Int randomAgentPos = new Vector2Int(Random.Range(0, mapWidth), Random.Range(0, mapHeight));
        List<Vector2Int> validGoals = new();
        for (int y = 0; y < _grid.GetHeight(); y++)
        {
            for (int x = 0; x < _grid.GetWidth(); x++)
            {
                var goalPos = new Vector2Int(x, y);
                float dist = Vector2Int.Distance(randomAgentPos, goalPos);
                if (dist >= minDistance)
                    validGoals.Add(goalPos);
            }
        }

        return validGoals.Count > 0
            ? (randomAgentPos, validGoals[Random.Range(0, validGoals.Count)])
            : (randomAgentPos, new Vector2Int(mapWidth - 1, mapHeight - 1));
    }

    private void DrawMap(Vector2Int agentPos, Vector2Int goalPos)
    {
        for (int y = 0; y < _grid.GetHeight(); y++)
        {
            for (int x = 0; x < _grid.GetWidth(); x++)
            {
                TileEntity prefabToSpawn;
                if (x == agentPos.x && y == agentPos.y)
                    prefabToSpawn = tileNPC;
                else if (x == goalPos.x && y == goalPos.y)
                    prefabToSpawn = tileGoal;
                else
                {
                    prefabToSpawn = RandomFloor();
                }
                
                var tileClone = Instantiate(prefabToSpawn, gridHolder.transform);
                tileClone.transform.localPosition = _grid.GetCellSize() * new Vector2(x, y);
                _grid.GetValue(x, y).SetTile(tileClone);
            }
        }
    }

    private TileEntity RandomFloor()
    {
        var randomF = Random.Range(0, 1f);
        if (randomF < 0.8f)
            return tileEmpty;
        return tileWall;
    }

    public enum SearchType
    {
        Astar,
        BFS,
    }
}
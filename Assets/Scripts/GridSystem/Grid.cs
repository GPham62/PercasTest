using System;
using UnityEngine;

namespace GridSystem
{
    public class Grid<TObject>
    {
        private int width;
        private int height;
        private float cellSize;
        private TObject[,] gridArray;
        private Vector2 gridOriginPosition;

        public Grid(int width, int height, float cellSize, Func<Grid<TObject>, int, int, TObject> createGridObject,
            Vector2 gridOriginPosition = default)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            gridArray = new TObject[width, height];
            this.gridOriginPosition = gridOriginPosition;
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = createGridObject(this, x, y);
                }
            }
        }

        public int GetWidth() => width;

        public int GetHeight() => height;

        public float GetCellSize() => cellSize;

        public void GetXY(Vector3 anchoredPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt(anchoredPosition.x / cellSize);
            y = Mathf.FloorToInt(anchoredPosition.y / cellSize);
        }

        public void SetValue(int x, int y, TObject value)
        {
            if (IsValidPosition(x, y))
            {
                gridArray[x, y] = value;
            }
        }

        public TObject GetValue(int x, int y)
        {
            if (IsValidPosition(x, y))
                return gridArray[x, y];
            else
                return default;
        }

        public TObject GetValue(Vector2Int gridPos)
            => GetValue(gridPos.x, gridPos.y);

        public Vector2 GetOriginPosition() => gridOriginPosition;
        
        public bool IsValidPosition(int x, int y)
            => x >= 0 && y >= 0 && x < width && y < height;

        public bool IsValidPosition(Vector2Int gridPos)
            => IsValidPosition(gridPos.x, gridPos.y);
    }
}
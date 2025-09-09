using UnityEngine;

namespace GridSystem
{
    public class TileGrid
    {
        private int _x;
        private int _y;
        private Grid<TileGrid> _grid;
        private TileEntity _tileEntity;

        public TileGrid(Grid<TileGrid> grid, int x, int y)
        {
            _x = x;
            _y = y;
            _grid = grid;
        }

        public void SetTile(TileEntity tileEntity) 
            => _tileEntity = tileEntity;

        public TileEntity GetTile() => _tileEntity;

        public Vector2Int Position => new Vector2Int(_x, _y);
    }
}

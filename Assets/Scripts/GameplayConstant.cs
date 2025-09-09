using UnityEngine;

public static class GameplayConstant
{
    public static readonly Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1,0), new Vector2Int(-1,0),
        new Vector2Int(0,1), new Vector2Int(0,-1)
    };
}
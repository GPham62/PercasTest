using System;
using UnityEngine;

namespace GridSystem
{
    public abstract class TileEntity : MonoBehaviour
    {
        public SpriteRenderer sprite;

        private void Awake()
        {
            if (sprite == null)
                sprite = GetComponent<SpriteRenderer>();
        }

        public void ChangeColor(Color color)
        {
            sprite.color = color;
        }

        public virtual bool IsWalkable() => true;
    }
}
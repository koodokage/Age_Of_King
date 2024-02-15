using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Abstract.Datas
{
    public abstract class AEntityData : ScriptableObject
    {
        [Header("USER INTERFACE")]
        [SerializeField] protected Sprite icon;
        [SerializeField] protected Vector2 aspectSize;
        [SerializeField] protected string label;
        [SerializeField] protected string description;
        [SerializeField] protected int price;

        [Header("GAME BOARD")]
        [SerializeField] protected Tile entityTile;
        [SerializeField, Range(1, 8)] protected int xDimension;
        [SerializeField, Range(1, 8)] protected int yDimension;

        public Tile GetTile { get => entityTile; }
        public int GetXDimension { get => xDimension; }
        public int GetYDimension { get => yDimension; }


        public Sprite GetIcon { get => icon; }
        public Vector2 GetAspectSize { get => aspectSize; }
        public string GetLabel { get => label; }
        public string GetDescription { get => description; }
        public int GetPrice { get => price; }


    }

}
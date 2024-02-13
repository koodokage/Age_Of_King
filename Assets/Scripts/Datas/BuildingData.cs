using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Components
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Building", order = 1)]
    public class BuildingData : ScriptableObject
    {
        public ABuilding BuildingPrefab;


        [Header("USER INTERFACE")]
        public Sprite Icon;
        public Vector2 AspectSize;
        public string BuildingLabel;

        [Header("GAME BOARD")]
        [SerializeField] Tile entityTile;
        [SerializeField] Tile groundTile;
        [SerializeField] int[] moveableRowIndexs;
        [SerializeField, Range(1, 8)] int xDimension;
        [SerializeField, Range(1, 8)] int yDimension;
        [SerializeField] Vector3Int placementOffset;

        public Tile GetTile { get => entityTile; }
        public Tile GetGroundTile { get => groundTile; }
        public int XDimension { get => xDimension; }
        public int YDimension { get => yDimension; }
        public Vector3Int GetPlacementOffset { get => placementOffset; }

        public bool IsRowMoveable(int rowIndex)
        {
            foreach (var index in moveableRowIndexs)
            {
                if (rowIndex == index)
                {
                    return false;
                }
            }

            return true;
        }

        public ABuilding CreateAndInitialize()
        {
            ABuilding buildingInstance = Instantiate(BuildingPrefab);
            buildingInstance.Initialize(this);
            return buildingInstance;
        }
    }
}

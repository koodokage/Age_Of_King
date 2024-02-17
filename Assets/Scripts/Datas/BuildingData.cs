using AgeOfKing.Abstract.Components;
using AgeOfKing.Abstract.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Building", order = 1)]
    public class BuildingData : AEntityData
    {
        [Header("BUILDING SETTINGS")]

        [SerializeField] protected ABuilding buildingPrefab;
        [SerializeField] protected Tile groundTile;
        [SerializeField] protected Vector3Int placementOffset;
        [SerializeField] protected int[] moveableRowIndexs;
        [SerializeField] protected int[] moveableColumnIndexs;

        public Tile GetGroundTile { get => groundTile; }
        public ABuilding GetBuildingPrefab { get => buildingPrefab; }
        public Vector3Int GetPlacementOffset { get => placementOffset; }


        public bool IsRowMoveable(int rowIndex,int columnIndex)
        {
            foreach (var index in moveableRowIndexs)
            {
                if (rowIndex == index)
                {
                    return true;
                }
            }

            foreach (var index in moveableColumnIndexs)
            {
                if (columnIndex == index)
                {
                    return true;
                }
            }

            return false;
        }

    }


}

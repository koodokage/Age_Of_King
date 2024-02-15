using AgeOfKing.Abstract.Components;
using AgeOfKing.Abstract.Datas;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Datas
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Building", order = 1)]
    public class BuildingData : AEntityData
    {
        [Header("BUILDING SETTINGS")]

        [SerializeField] protected ABuilding buildingPrefab;
        [SerializeField] protected Tile groundTile;
        [SerializeField] protected Vector3Int placementOffset;
        [SerializeField] protected int[] moveableRowIndexs;

        public Tile GetGroundTile { get => groundTile; }
        public ABuilding GetBuildingPrefab { get => buildingPrefab; }
        public Vector3Int GetPlacementOffset { get => placementOffset; }


        public bool IsRowMoveable(int rowIndex)
        {
            foreach (var index in moveableRowIndexs)
            {
                if (rowIndex == index)
                {
                    return true;
                }
            }

            return false;
        }

    }

}

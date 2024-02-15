using UnityEngine;

namespace AgeOfKing.Datas
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ManufacturerBuilding", order = 1)]
    public class ManufacturerBuildingData : ScriptableObject
    {
        [Header("PRODUCE SETTINGS")]
        [SerializeField] protected UnitData[] units;
        public UnitData[] GetUnits { get => units; }
        [SerializeField] protected int[] spawnableRowIndexs;
        public int[] GetSpawnableRowIndexs { get => spawnableRowIndexs; }

    }
}

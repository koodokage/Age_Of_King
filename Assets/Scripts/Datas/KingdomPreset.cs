using AgeOfKing.Data;
using UnityEngine;

namespace AgeOfKing.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Kingdom", order = 2)]
    public class KingdomPreset : ScriptableObject
    {
        public enum Kingdom { HUMAN,GOBLIN }

        [Header("Setup")]
        [SerializeField] Kingdom kigdomType;
        [SerializeField] UnitData kingdomKing;
        [SerializeField] UnitData[] kingdomUnits;
        [SerializeField] BuildingData[] kingdomBuildings;

        [Header("Start Resources")]
        [SerializeField] int gold;
        [SerializeField] int populationCapacity;
        [SerializeField] int moveRights;
        [SerializeField] int kingMoveRights;

        public Kingdom GetKigdomType { get => kigdomType;}
        public UnitData GetKigdomKing { get => kingdomKing; }
        public UnitData[] GetKigdomUnits { get => kingdomUnits; }
        public BuildingData[] GetKingdomBuildings { get => kingdomBuildings; }
        public int GetGold { get => gold;  }
        public int GetPopulationCapacity { get => populationCapacity;  }
        public int GetMoveRights { get => moveRights;}
        public int GetKingMoveRights { get => kingMoveRights; }
    }


}
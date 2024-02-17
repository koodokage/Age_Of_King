using AgeOfKing.Abstract.Components;
using AgeOfKing.Abstract.Data;
using AgeOfKing.Data;
using UnityEngine;

namespace AgeOfKing.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Unit", order = 2)]
    public class UnitData : AEntityData
    {
        [SerializeField] AUnit unitPrefab;
        [Header("CHARACTER STATS")]
        [SerializeField] protected CharacterStatData[] stats;

        public AUnit GetPrefab { get => unitPrefab; }
        public CharacterStatData[] GetStats { get => stats; }

        public bool TryGetValueByGenre(CharacterStatGenre statGenre,out int value)
        {
            value = 0;
            foreach (var stat in stats)
            {
                if(stat.GetGenre == statGenre)
                {
                    value = stat.GetValue;
                    return true;
                }
            }

            return false;
        }

    }

}
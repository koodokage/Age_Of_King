using System;
using UnityEngine;

namespace AgeOfKing.Data
{

    public enum StatUsage { ONCE, PERTURN }
    public enum ValueGenre { GOLD, POPULATION,POPCAPACITY, MOVERIGHTS, KINGMOVERIGHTS }

    [Serializable]
    public struct EntityStat
    {
        [SerializeField] private StatUsage usage;
        [SerializeField] private ValueGenre genre;
        [SerializeField] private int value;

        public StatUsage GetUsage { get => usage;  }
        public ValueGenre GetGenre { get => genre;  }
        public int GetValue { get => value; }
    }


}

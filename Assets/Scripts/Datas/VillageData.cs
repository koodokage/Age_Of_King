using System;

namespace AgeOfKing.Data
{
    [Serializable]
    public class VillageData
    {
        public VillageData(int gold, int currentPopulation, int populationCapacity, int moveRights, int kingMoveRights)
        {
            Gold = gold;
            CurrentPopulation = currentPopulation;
            PopulationCapacity = populationCapacity;
            MoveRights = moveRights;
            KingMoveRights = kingMoveRights;
        }

        public int Gold;
        public int CurrentPopulation;
        public int PopulationCapacity;
        public int MoveRights;
        public int KingMoveRights;


        public int Population { get => PopulationCapacity - CurrentPopulation; }

    }
}

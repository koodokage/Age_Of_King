using AgeOfKing.Data;

namespace AgeOfKing.Data
{
    public class PopulationCapacityDataModel : AVillageDataModel
    {
        public PopulationCapacityDataModel(VillageData data, ValueGenre genre) : base(data, genre)
        {

        }

        public override bool CheckEnough(int amount)
        {
            return false;
        }

        public override void Increase(int amount)
        {
            villageData.PopulationCapacity += amount;
            base.Increase(amount);
        }

        public override void Decrease(int amount)
        {
            villageData.PopulationCapacity -= amount;
            base.Decrease(amount);
        }

    }

}

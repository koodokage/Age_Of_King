using AgeOfKing.Data;

namespace AgeOfKing.Data
{
    public class PopulationDataModel : AVillageDataModel
    {
        public PopulationDataModel(VillageData data, ValueGenre genre) : base(data, genre)
        {

        }

        public override bool CheckEnough(int amount)
        {
            return false;
        }

        public override void Increase(int amount)
        {
            villageData.CurrentPopulation += amount;
            base.Increase(amount);
        }

        public override void Decrease(int amount)
        {
            villageData.CurrentPopulation -= amount;
            base.Decrease(amount);
        }

    }

}

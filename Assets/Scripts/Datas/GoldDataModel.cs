using AgeOfKing.Data;

namespace AgeOfKing.Data
{
    public class GoldDataModel : AVillageDataModel
    {
        public GoldDataModel(VillageData data, ValueGenre genre) :base(data,genre)
        {
        }

        public override bool CheckEnough(int amount)
        {
            return (villageData.Gold - amount >= 0) ? true : false;
        }

        public override void Increase(int amount)
        {
            villageData.Gold += amount;
            base.Increase(amount);
        }

        public override void Decrease(int amount)
        {
            villageData.Gold -= amount;
            base.Decrease(amount);
        }

    }





}

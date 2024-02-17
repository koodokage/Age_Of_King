using AgeOfKing.Data;

namespace AgeOfKing.Data
{
    public class MoveRightsDataModel : AVillageDataModel
    {
        public MoveRightsDataModel(VillageData data, ValueGenre genre) : base(data, genre)
        {

        }
        public override bool CheckEnough(int amount)
        {
            return (villageData.MoveRights - amount >= 0 )? true:false;
        }

        public override void Decrease(int amount)
        {
            villageData.MoveRights -= amount;
            base.Decrease(amount);
        }

        public override void Increase(int amount)
        {
            villageData.MoveRights += amount;
            base.Increase(amount);
        }

    }





}

namespace AgeOfKing.Data
{
    public class KingMoveRightsDataModel : AVillageDataModel
    {
        public KingMoveRightsDataModel(VillageData data, ValueGenre genre) : base(data, genre)
        {

        }
        public override bool CheckEnough(int amount)
        {
            return (villageData.KingMoveRights - amount >= 0) ? true : false;
        }

        public override void Decrease(int amount)
        {
            villageData.KingMoveRights -= amount;
            base.Decrease(amount);
        }

        public override void Increase(int amount)
        {
            villageData.KingMoveRights += amount;
            base.Increase(amount);
        }

    }





}

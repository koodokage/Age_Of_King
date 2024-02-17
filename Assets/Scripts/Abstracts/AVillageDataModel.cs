using AgeOfKing.Data;
using System;

namespace AgeOfKing.Data
{
    public abstract class AVillageDataModel
    {
        public enum DataModelChangeWay
        {
            NONE,
            INCREASE,
            DECREASE
        }
        protected readonly  VillageData villageData;
        protected readonly ValueGenre dataGenre;
        public event Action<VillageData, DataModelChangeWay> OnChange;

        public AVillageDataModel(VillageData data, ValueGenre genre)
        {
            villageData = data;
            dataGenre = genre;
            OnChange?.Invoke(villageData,DataModelChangeWay.NONE);
        }

        public void ClearBindings()
        {
            OnChange = null;
        }

        public void RefreshView()
        {
            OnChange?.Invoke(villageData, DataModelChangeWay.INCREASE);
        }

        public void ResetMoveRights(KingdomPreset presetData)
        {
            villageData.MoveRights = presetData.GetMoveRights;
            OnChange?.Invoke(villageData,DataModelChangeWay.INCREASE);
        }

        public virtual void Increase(int amount)
        {
            OnChange?.Invoke(villageData, DataModelChangeWay.INCREASE);
        }

        public virtual void Decrease(int amount)
        {
            OnChange?.Invoke(villageData, DataModelChangeWay.DECREASE);
        }

        public abstract bool CheckEnough(int amount);

    }



}

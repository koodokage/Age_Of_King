using AgeOfKing.Abstract.Data;
using AgeOfKing.Systems;
using AgeOfKing.UI;

public abstract class AEntityProducerButton<T> : AVillageDataView where T : AEntityData
{
    public IPlayer ownerPlayer { get; private set; }
    public abstract void InitializeData(T entityData, IPlayer player);

    protected  void InitializeListener(IPlayer player)
    {
        ModelViewController.GetInstance.OnPlayerVillageDataChanged += VillageDataChanged;
        ownerPlayer = player;
        VillageDataChanged(ownerPlayer.GetVillage.villageData);
    }

    protected virtual void OnDisable()
    {
        ModelViewController.GetInstance.OnPlayerVillageDataChanged -= VillageDataChanged;
    }
}

using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.UI;
using UnityEngine;

namespace AgeOfKing.Components
{
    public class BuildingProduceButtonFactory : AProducerUIFactory<BuildingData, BuildingProduceButton>
    {
        [SerializeField] BuildingProduceButton prefab_BuildingButton;

        public override BuildingProduceButton GetProducerUI(BuildingData produceData,Transform parent,IPlayer player)
        {
            BuildingProduceButton instance = null;
            if (_pool.Count > 0)
            {
                instance = _pool.Pop();
                instance.transform.SetParent(parent);
                instance.gameObject.SetActive(true);
            }
            else
            {
                instance = Instantiate(prefab_BuildingButton, parent);
            }

            instance.InitializeData(produceData, player);
            return instance;
        }

        public override void Release(BuildingProduceButton unitButton)
        {
            unitButton.gameObject.SetActive(false);
            unitButton.transform.SetParent(transform);
            _pool.Push(unitButton);
        }
    }

}

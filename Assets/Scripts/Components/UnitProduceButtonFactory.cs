using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.UI;
using UnityEngine;

namespace AgeOfKing.Components
{
    public class UnitProduceButtonFactory : AProducerUIFactory<UnitData, UnitProduceButton>
    {
        [SerializeField] UnitProduceButton prefab_unitButton;

        public override UnitProduceButton GetProducerUI(UnitData produceData,Transform parent,IPlayer player)
        {
            UnitProduceButton instance = null;
            if (_pool.Count > 0)
            {
                instance = _pool.Pop();
                instance.transform.SetParent(parent);
                instance.gameObject.SetActive(true);
            }
            else
            {
                instance = Instantiate(prefab_unitButton,parent);
            }

            instance.InitializeData(produceData, player);
            return instance;
        }

        public override void Release(UnitProduceButton unitButton)
        {
            unitButton.gameObject.SetActive(false);
            unitButton.transform.SetParent(transform);
            _pool.Push(unitButton);
        }
    }

}

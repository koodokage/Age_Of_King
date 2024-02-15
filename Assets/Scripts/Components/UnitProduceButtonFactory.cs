using AgeOfKing.Abstract.Components;
using AgeOfKing.Datas;
using UnityEngine;

namespace AgeOfKing.Components
{
    public class UnitProduceButtonFactory : AProducerUIFactory<UnitData, AEntityProducerButton>
    {
        [SerializeField] AEntityProducerButton prefab_unitButton;

        public override AEntityProducerButton GetProducerUI(UnitData produceData,Transform parent)
        {
            AEntityProducerButton instance = null;
            if (_pool.Count > 0)
            {
                instance = _pool.Pop();
                instance.gameObject.SetActive(true);
            }
            else
            {
                instance = Instantiate(prefab_unitButton,parent);
            }

            instance.InitializeData(produceData);
            return instance;
        }

        public override void Release(AEntityProducerButton unitButton)
        {
            unitButton.gameObject.SetActive(false);
            unitButton.transform.SetParent(null);
            _pool.Push(unitButton);
        }
    }

}

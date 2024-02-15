using AgeOfKing.Abstract.Datas;
using System.Collections.Generic;
using UnityEngine;

namespace AgeOfKing.Abstract.Components
{
    public abstract class AProducerUIFactory<T, K> : SingleBehaviour<AProducerUIFactory<T, K>> where T : AEntityData where K : AEntityProducerButton
    {
        protected Stack<K> _pool;

        protected override void Awake()
        {
            base.Awake();
            _pool = new Stack<K>();
        }

        public abstract K GetProducerUI(T produceData,Transform parent);

        public abstract void Release(K unitButton);
    }

}
using AgeOfKing.Abstract.Data;
using AgeOfKing.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace AgeOfKing.Abstract.Components
{
    public abstract class AProducerUIFactory<T, K> : ASingleBehaviour<AProducerUIFactory<T, K>> where T : AEntityData where K : AEntityProducerButton<T>
    {
        protected Stack<K> _pool;

        protected override void Awake()
        {
            base.Awake();
            _pool = new Stack<K>();
        }

        public abstract K GetProducerUI(T produceData,Transform parent,IPlayer player);

        public abstract void Release(K unitButton);

        public abstract float GetPrefabHeight { get; }
    }

}
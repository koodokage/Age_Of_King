using AgeOfKing.Abstract.Datas;
using UnityEngine;
namespace AgeOfKing.Abstract.Components
{
    public  abstract class AEntityFactory<T,K> : SingleBehaviour<AEntityFactory<T,K>> where T : AEntityData where K : MonoBehaviour
    {
        public abstract K Produce(T entityData);
    }

}
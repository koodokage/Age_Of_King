using AgeOfKing.Abstract.Data;
using AgeOfKing.Systems;
using UnityEngine;
namespace AgeOfKing.Abstract.Components
{
    public  abstract class AEntityFactory<T,K> : ASingleBehaviour<AEntityFactory<T,K>> where T : AEntityData where K : MonoBehaviour
    {
        public abstract K Produce(T entityData,IPlayer manager);
    }

}
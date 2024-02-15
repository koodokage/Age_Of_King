using AgeOfKing.Abstract.Datas;
using UnityEngine;

namespace AgeOfKing.Abstract.Components
{
    public abstract class AMapEntity : MonoBehaviour
    {
        protected AEntityData _baseData;

        public AEntityData GetBaseData { get => _baseData; }

        public abstract void Draw(Vector3Int cellLocation);
    }
}

using AgeOfKing.Abstract.Data;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using UnityEngine;

namespace AgeOfKing.Abstract.Components
{
    public abstract class AMapEntity<T> : MonoBehaviour where T : AEntityData
    {
        protected IPlayer owner;

        protected AEntityData _baseData;

        public AEntityData GetBaseData { get => _baseData; }

        public IPlayer GetOwnerPlayer { get => owner; }

        protected Vector3Int _currentCellLocation;
        public Vector3Int GetCurrentCellLocation { get => _currentCellLocation; }

        public abstract void Draw(Vector3Int cellLocation);
        public abstract void InitializeData(T data, IPlayer player);
        public void Erase()
        {
            Map.GetInstance.GetUnitMap.SetTile(_currentCellLocation,null);
            Destroy(gameObject);
            // or ? Return to factory
        }

    }

  
}

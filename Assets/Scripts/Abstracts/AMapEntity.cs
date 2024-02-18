using AgeOfKing.Abstract.Data;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Abstract.Components
{
    public abstract class AMapEntity<T> : MonoBehaviour where T : AEntityData
    {
        protected IPlayer owner;

        public IPlayer GetOwnerPlayer { get => owner; }

        protected AEntityData _baseData;

        public AEntityData GetBaseData { get => _baseData; }

        protected int currentHealth;
        public int CurrentHealth { get => currentHealth; }

        protected Vector3Int currentCellLocation;
        public Vector3Int GetCurrentCellLocation { get => currentCellLocation; }

        public abstract void Draw(Vector3Int cellLocation);
        public virtual void InitializeData(T data, IPlayer player)
        {
            currentHealth = data.GetEntityHealth;

        }
        public virtual void Erase()
        {
            Map.GetInstance.GetUnitMap.SetTile(currentCellLocation,null);
            Destroy(gameObject);
            // or ? Return to factory
        }

        protected IEnumerator FlickerAnimation(Tilemap tileMap, Color hgColor, Vector3Int location, Action onEnd = null)
        {
            tileMap.SetTileFlags(location, UnityEngine.Tilemaps.TileFlags.None);

            tileMap.SetColor(location, hgColor);
            yield return AnimationUtils.waitFor_OneHundredMS;
            tileMap.SetColor(location, Color.white);
            yield return AnimationUtils.waitFor_OneHundredMS;
            tileMap.SetColor(location, hgColor);
            yield return AnimationUtils.waitFor_OneHundredMS;
            tileMap.SetColor(location, Color.white);

            tileMap.SetTileFlags(location, UnityEngine.Tilemaps.TileFlags.LockColor);

            onEnd?.Invoke();

        }

    }

  
}

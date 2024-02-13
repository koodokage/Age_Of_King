using AgeOfKing.Components;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Datas
{
    public class MapEntityData : SingleBehaviour<MapEntityData>
    {
        [SerializeField] Tilemap obstacleMap;

        Dictionary<Vector3Int, bool> staticObstacleDB;
        Dictionary<Vector3Int, bool> buildingsDB;
        Dictionary<Vector3Int, bool> dynamicsDB;
        Dictionary<Vector3Int, ABuilding> buildingInstancesDB;

        private void Start()
        {
            Assert.IsNotNull(obstacleMap);

            staticObstacleDB = new Dictionary<Vector3Int, bool>();
            buildingsDB = new Dictionary<Vector3Int, bool>();
            dynamicsDB = new Dictionary<Vector3Int, bool>();
            buildingInstancesDB = new Dictionary<Vector3Int, ABuilding>();

            var cells = GetAllPaintedTiles();
            foreach (var item in cells)
            {
                staticObstacleDB.Add(item, true);
            }

            Debug.Log("[OBSTACLE DATA INITIALZIED]");
        }

        public void AddBuildingData(Vector3Int cell,ABuilding building)
        {
            buildingsDB.TryAdd(cell, true);
            buildingInstancesDB.TryAdd(cell, building);
        }

        public void AddObstacleData(Vector3Int cell)
        {
            staticObstacleDB.TryAdd(cell, true);
        }

        public void AddDynamicData(Vector3Int cell)
        {
            dynamicsDB.TryAdd(cell, true);
        }

        public void RemoveDynamicData(Vector3Int cell)
        {
            dynamicsDB.Remove(cell);
        }

        public bool IsPlaceBlocked(Vector3Int cell)
        {
            if (staticObstacleDB.ContainsKey(cell)) return true;
            if (buildingsDB.ContainsKey(cell)) return true;
            if (dynamicsDB.ContainsKey(cell)) return true;

            return false;
        }

        public bool IsPlaceBulding(Vector3Int cell,out ABuilding building)
        {
            if (buildingInstancesDB.TryGetValue(cell, out building))
            {
                return true;
            }

            return false;
        }

        IEnumerable<Vector3Int> GetAllPaintedTiles()
        {
            var bound = obstacleMap.cellBounds;
            for (int x = bound.min.x; x < bound.max.x; x++)
            {
                for (int y = bound.min.y; y < bound.max.y; y++)
                {
                    var cellPosition = new Vector3Int(x, y, 0);
                    var sprite = obstacleMap.GetSprite(cellPosition);
                    var tile = obstacleMap.GetTile(cellPosition);

                    if (tile == null && sprite == null)
                    {
                        continue;
                    }

                    yield return cellPosition;
                }
            }

        }
    }

}

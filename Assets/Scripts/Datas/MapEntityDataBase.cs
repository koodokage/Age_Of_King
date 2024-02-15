using AgeOfKing.Abstract.Components;
using AgeOfKing.AStar;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Datas
{
    public class MapEntityDataBase : SingleBehaviour<MapEntityDataBase>
    {
        Dictionary<Vector3Int, bool> staticObstacleDB;
        Dictionary<Vector3Int, bool> buildingExtrudedDB;
        Dictionary<Vector3Int, AUnit> unitDB;
        Dictionary<Vector3Int, ABuilding> buildingInstancesDB;

        private void Start()
        {
            PathFinder.Start();
            staticObstacleDB = new Dictionary<Vector3Int, bool>();
            buildingExtrudedDB = new Dictionary<Vector3Int, bool>();
            unitDB = new Dictionary<Vector3Int, AUnit>();
            buildingInstancesDB = new Dictionary<Vector3Int, ABuilding>();

            var cells = GetAllPaintedTiles();
            foreach (var cell in cells)
            {
                AddObstacleData(cell);
            }

        }

        public void AddBuildingData(Vector3Int cell,ABuilding building)
        {
            buildingInstancesDB.TryAdd(cell, building);
        }

        public void AddBuildingExtrudeData(Vector3Int cell)
        {
            buildingExtrudedDB.TryAdd(cell, true);
            PathFinder.UpdateMovementObstacles(cell);
        }

        public void AddObstacleData(Vector3Int cell)
        {
            staticObstacleDB.TryAdd(cell, true);
            PathFinder.UpdateMovementObstacles(cell);
        }

        public void AddUnitData(Vector3Int cell,AUnit unit)
        {
            unitDB.TryAdd(cell, unit);
            PathFinder.UpdateMovementObstacles(cell);
        }

        public void RemoveUnitData(Vector3Int cell)
        {
            unitDB.Remove(cell);
            PathFinder.RemoveMovementObstacles(cell);
        }

        public bool IsTileBlocked(Vector3Int cell)
        {
            if (staticObstacleDB.ContainsKey(cell)) return true;
            if (buildingInstancesDB.ContainsKey(cell)) return true;
            if (unitDB.ContainsKey(cell)) return true;

            return false;
        }

        public bool IsTileSpawnable(Vector3Int cell)
        {
            if (staticObstacleDB.ContainsKey(cell)) return false;
            if (unitDB.ContainsKey(cell)) return false;

            return true;
        }

        public bool IsTileContainBulding(Vector3Int cell,out ABuilding building)
        {
            if (buildingInstancesDB.TryGetValue(cell, out building))
            {
                return true;
            }

            return false;
        }

        public bool IsTileContainSelecteable(Vector3Int cell, out ISelectable selectable)
        {
            selectable = null;

            if (unitDB.TryGetValue(cell, out AUnit unit))
            {
                unit.TryGetComponent(out selectable);
                return true;
            }

            if (buildingInstancesDB.TryGetValue(cell, out ABuilding building))
            {
                building.TryGetComponent(out selectable);
                return true;
            }

            return false;

        }

        public bool IsTileMoveable(Vector3Int cell)
        {
            if (staticObstacleDB.ContainsKey(cell))
            {
                return false;
            }

            if (buildingExtrudedDB.ContainsKey(cell))
            {
                return false;
            }

            if (unitDB.ContainsKey(cell))
            {
                return false;
            }

            return true;

        }

        public bool IsTileContainUnit(Vector3Int cell, out AUnit unit)
        {
            if (unitDB.TryGetValue(cell, out  unit))
            {
                return true;
            }

            return false;

        }

        IEnumerable<Vector3Int> GetAllPaintedTiles()
        {
            Tilemap obstacleMap = Maps.GetInstance.GetObstacleMap;
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

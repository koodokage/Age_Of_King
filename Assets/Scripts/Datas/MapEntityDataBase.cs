using AgeOfKing.Abstract.Components;
using AgeOfKing.AStar;
using AgeOfKing.Systems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Data
{
    /// <summary>
    /// game map entity data base (exp :checking tile,get unit and obstacle/building tiles )
    /// </summary>
    public static class MapEntityDataBase 
    {
        /// <summary>
        /// Partial dictionaries for quick search also for layer detections
        /// </summary>
        static Dictionary<Vector3Int, bool> staticObstacleDB;
        static Dictionary<Vector3Int, ABuilding> buildingExtrudedDB;
        static Dictionary<Vector3Int, AUnit> unitDB;
        static Dictionary<Vector3Int, IHittable> hittableDB;
        static Dictionary<Vector3Int, ABuilding> buildingInstancesDB;

        public static void Initiliaze()
        {

            staticObstacleDB = new Dictionary<Vector3Int, bool>();
            buildingExtrudedDB = new Dictionary<Vector3Int, ABuilding>();
            unitDB = new Dictionary<Vector3Int, AUnit>();
            buildingInstancesDB = new Dictionary<Vector3Int, ABuilding>();
            hittableDB = new Dictionary<Vector3Int, IHittable>();

            var cells = GetAllPaintedTiles();
            foreach (var cell in cells)
            {
                AddObstacleData(cell);
            }
        }


        /// <summary>
        /// Building covered tile data
        /// </summary>
        /// <param name="cell">tile location</param>
        /// <param name="building">building instance</param>
        public static void AddBuildingData(Vector3Int cell,ABuilding building)
        {
            buildingInstancesDB.TryAdd(cell, building);
        }

        public static void RemoveBuildingData(Vector3Int cell)
        {
            buildingInstancesDB.Remove(cell);
        }

        /// <summary>
        /// Building covered non walkable tile data
        /// </summary>
        /// <param name="cell"></param>
        public static void AddBuildingBlockedData(Vector3Int cell,ABuilding building)
        {
            buildingExtrudedDB.TryAdd(cell, building);
            PathFinder.UpdateMovementObstacles(cell);
            if (building.TryGetComponent(out IHittable hittable))
            {
                hittableDB.TryAdd(cell, hittable);
            }
        }

        public static void RemoveBuildingBlockedData(Vector3Int cell)
        {
            buildingExtrudedDB.Remove(cell);
            hittableDB.Remove(cell);
            PathFinder.RemoveMovementObstacles(cell);
        }

        /// <summary>
        /// Static obstacles in map
        /// </summary>
        /// <param name="cell">tile location </param>
        public static void AddObstacleData(Vector3Int cell)
        {
            staticObstacleDB.TryAdd(cell, true);
            PathFinder.UpdateMovementObstacles(cell);
        }

        /// <summary>
        /// AUnit covered tile
        /// </summary>
        /// <param name="cell">tile location</param>
        /// <param name="unit">unit instance</param>
        public static void AddUnitData(Vector3Int cell,AUnit unit)
        {
            unitDB.TryAdd(cell, unit);
            PathFinder.UpdateMovementObstacles(cell);

            if(unit.TryGetComponent(out IHittable hittable))
            {
                hittableDB.TryAdd(cell, hittable);
            }
        }

        /// <summary>
        /// AUnit released tile
        /// </summary>
        /// <param name="cell">tile location</param>
        public static void RemoveUnitData(Vector3Int cell)
        {
            unitDB.Remove(cell);
            hittableDB.Remove(cell);
            PathFinder.RemoveMovementObstacles(cell);
        }

        /// <summary>
        /// check all obstacle and covered tile (exp : building placement)
        /// </summary>
        /// <param name="cell">tile location</param>
        /// <returns></returns>
        public static bool IsTileBlocked(Vector3Int cell)
        {
            if (staticObstacleDB.ContainsKey(cell)) return true;
            if (buildingInstancesDB.ContainsKey(cell)) return true;
            if (unitDB.ContainsKey(cell)) return true;

            return false;
        }

        /// <summary>
        /// check procedural tile in unit datas
        /// </summary>
        /// <param name="cell">tile location</param>
        /// <returns></returns>
        public static bool IsTileSpawnable(Vector3Int cell)
        {
            if (unitDB.ContainsKey(cell)) return false;

            return true;
        }


        /// <summary>
        /// check and return AUnit in tile
        /// </summary>
        /// <param name="cell">tile location</param>
        /// <param name="unit">unit instance</param>
        /// <returns></returns>
        public static bool IsTileContainUnit(Vector3Int cell, out AUnit unit)
        {
            if (unitDB.TryGetValue(cell, out  unit))
            {
                return true;
            }

            return false;

        }

        public static bool IsTileContainExtrudedBuilding(Vector3Int cell, out ABuilding building)
        {
            if (buildingExtrudedDB.TryGetValue(cell, out building))
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// check and return hittable in tile
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="hittableUnit"></param>
        /// <returns></returns>
        public static bool IsTileContainHittable(Vector3Int cell, out IHittable hittableUnit)
        {
            if (hittableDB.TryGetValue(cell, out hittableUnit))
            {
                return true;
            }

            if (buildingExtrudedDB.TryGetValue(cell, out ABuilding  building))
            {
                building.TryGetComponent(out hittableUnit);
                Debug.LogError("BUILDING HITTABLE");
                return true;
            }

            Debug.LogError("NOTHIING HITTABLE");

            return false;

        }

        /// <summary>
        /// return a selectable map entity
        /// </summary>
        /// <param name="cell">tile location</param>
        /// <param name="selectable">selectable entity</param>
        /// <returns></returns>
        public static bool IsTileContainSelecteable(Vector3Int cell, out ISelectable selectable)
        {
            selectable = null;

            if (unitDB.TryGetValue(cell, out AUnit unit))
            {
                unit.TryGetComponent(out selectable);
                return true;
            }

            if (buildingExtrudedDB.TryGetValue(cell, out ABuilding building))
            {
                building.TryGetComponent(out selectable);
                return true;
            }

            return false;

        }


        /// <summary>
        /// load all brushed tile in target map
        /// </summary>
        /// <returns></returns>
        static IEnumerable<Vector3Int> GetAllPaintedTiles()
        {
            Tilemap obstacleMap = Map.GetInstance.GetObstacleMap;
       
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

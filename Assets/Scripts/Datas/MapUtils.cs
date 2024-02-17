using AgeOfKing.Data;
using AgeOfKing.Systems;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Utils
{
    public static class MapUtils
    {
        public static int2[] MovementWays;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitializeMovementWay()
        {
            Debug.Log("MOVEMNT WAY INITIALIZED");
            MovementWays = new int2[8];
            MovementWays[0] = new int2(0, 1);
            MovementWays[1] = new int2(1, 1);
            MovementWays[2] = new int2(1, 0);
            MovementWays[3] = new int2(1, -1);
            MovementWays[4] = new int2(0, -1);
            MovementWays[5] = new int2(-1, -1);
            MovementWays[6] = new int2(-1, 0);
            MovementWays[7] = new int2(-1, 1);
        }

        public static void GetGroundMapBounds(out int xMin, out int xMax, out int yMin, out int yMax)
        {
            Tilemap groundMap = Map.GetInstance.GetGroundMap;
            var boundc = groundMap.cellBounds;
            xMin = boundc.min.x;
            xMax = boundc.max.x;
            yMin = boundc.min.y;
            yMax = boundc.max.y;
        }

        public static Vector3Int GetKingStartLocation(SIDE side)
        {
            Tilemap groundMap = Map.GetInstance.GetGroundMap;
            var boundc = groundMap.cellBounds;

            int xMin = boundc.min.x;
            int xMax = boundc.max.x;
            int yMin = boundc.min.y;
            int yMax = boundc.max.y;

            Vector3Int vector = Vector3Int.zero;

            switch (side)
            {
                case SIDE.LEFT:
                    vector = new Vector3Int(xMin, (yMin + yMax) / 2, 0);
                    break;
                case SIDE.RIGHT:
                    vector = new Vector3Int(xMax - 1, (yMin + yMax) / 2, 0);
                    break;
            }

            return vector;
        }
    }

}
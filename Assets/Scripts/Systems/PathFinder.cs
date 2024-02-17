using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace AgeOfKing.AStar
{
    public class PathFinder
    {
        static Dictionary<int2,bool> _obstacles = new Dictionary<int2, bool>();
        static AStarNode start = new AStarNode { coordinate = int2.zero, parent = int2.zero, gScore = int.MaxValue, hScore = int.MaxValue };
        static AStarNode end = new AStarNode { coordinate = int2.zero, parent = int2.zero, gScore = int.MaxValue, hScore = int.MaxValue };
        static int safeGuard = 1000;
        static int pathDistance;

        


        public static bool GetPath(Vector3Int start,Vector3Int end,int distance , out Vector3Int[] pathOrdered, out Vector3Int lastCell,out int iteratedDistance)
        {
            lastCell = start;
            Stack<Vector3Int> aStarPath = new Stack<Vector3Int>();
            InitializeStartNode(start);
            InitializeEndNode(end);
            iteratedDistance = ExecuteAStarJob(distance, ref aStarPath,ref lastCell);
            pathDistance = distance;

            int pathLength = aStarPath.Count;
            pathOrdered = new Vector3Int[pathLength];

            if (pathLength == 0)
                return false;


            while (aStarPath.Count > 0)
            {
                pathLength--;
                Vector3Int loc = aStarPath.Pop();
                pathOrdered[pathLength] = loc;
                if (pathLength == 0)
                    break;
            }

            return true;
        }


        public static void InitializeStartNode(Vector3Int mouseCell)
        {
            int2 coord = new int2 { x = mouseCell.x, y = mouseCell.y };
            start.coordinate = coord;
        }

        public static void InitializeEndNode(Vector3Int mouseCell)
        {
            int2 coord = new int2 { x = mouseCell.x, y = mouseCell.y };
            end.coordinate = coord;
        }

        public static void UpdateMovementObstacles(Vector3Int cellLocation)
        {
            int2 coord = new int2 { x = cellLocation.x, y = cellLocation.y };

            if (!_obstacles.ContainsKey(coord))
                _obstacles.Add(coord, true);

        }

        public static void RemoveMovementObstacles(Vector3Int cellLocation)
        {
            int2 coord = new int2 { x = cellLocation.x, y = cellLocation.y };

            if (_obstacles.ContainsKey(coord))
                _obstacles.Remove(coord);

        }

        static int ExecuteAStarJob(int distance,ref Stack<Vector3Int> path,ref Vector3Int lastLocation)
        {
            // jobified datas
            NativeHashMap<int2, bool> init_obstacleSet = new NativeHashMap<int2, bool>(_obstacles.Count, Allocator.TempJob);
            NativeHashMap<int2, AStarNode> init_moveableNodes = new NativeHashMap<int2, AStarNode>(safeGuard, Allocator.TempJob);
            NativeHashMap<int2, AStarNode> init_searchableSet = new NativeHashMap<int2, AStarNode>(safeGuard, Allocator.TempJob);
            NativeArray<int2> init_moveOffsets = new NativeArray<int2>(8, Allocator.TempJob);

            foreach (int2 x in _obstacles.Keys)
            {
                init_obstacleSet.Add(x, true);
            }

            //create&initialize target job
            AStarJob aStar = new AStarJob
            {
                startingNode = start,
                endingNode = end,
                obstacleSet = init_obstacleSet,
                moveSet = init_moveableNodes,
                searchSet = init_searchableSet,
                movementWay = init_moveOffsets,
                redLine = safeGuard,
                maxPathLength = pathDistance
            };

            // job starting
            JobHandle jHandle = aStar.Schedule();
            jHandle.Complete();

            //resutls ->

            NativeArray<AStarNode> nodeArray = init_moveableNodes.GetValueArray(Allocator.TempJob);
            // valid path
            if (init_moveableNodes.ContainsKey(end.coordinate))
            {
                int2 currentCoord = end.coordinate;
                Vector3Int currentTile = new Vector3Int(currentCoord.x, currentCoord.y, 0);
                while (!currentCoord.Equals(start.coordinate))
                {
                    path.Push(currentTile);
                    currentCoord = init_moveableNodes[currentCoord].parent;
                    currentTile = new Vector3Int(currentCoord.x, currentCoord.y, 0);
                }
            }




            //release memory
            init_moveableNodes.Dispose();
            init_obstacleSet.Dispose();
            init_searchableSet.Dispose();
            init_moveOffsets.Dispose();
            nodeArray.Dispose();


            return aStar.currentPathLength;

        }
    }
}

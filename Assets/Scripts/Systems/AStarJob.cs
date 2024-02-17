using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

namespace AgeOfKing.AStar
{
    [BurstCompile(CompileSynchronously = true)]
    public struct AStarJob : IJob
    {
        //jobified datas

        //seasoned obstacles
        public NativeHashMap<int2, bool> obstacleSet;
        //qualified nodes for iterate movement
        public NativeHashMap<int2, AStarNode> moveSet;
        //searchable nodes for correct path
        public NativeHashMap<int2, AStarNode> searchSet;
        //node movement ways
        public NativeArray<int2> movementWay;

        public AStarNode startingNode;
        public AStarNode endingNode;

        public int redLine;
        public int maxPathLength;
        public int currentPathLength;



        public void Execute()
        {
            AStarNode current = startingNode;
            current.gScore = 0;
            current.hScore = GetPisagorDistance(current.coordinate, endingNode.coordinate);
            searchSet.TryAdd(current.coordinate, current);

            //initialize Movement Ways
            movementWay[0] = new int2(0, 1);
            movementWay[1] = new int2(1, 1);
            movementWay[2] = new int2(1, 0);
            movementWay[3] = new int2(1, -1);
            movementWay[4] = new int2(0, -1);
            movementWay[5] = new int2(-1, -1);
            movementWay[6] = new int2(-1, 0);
            movementWay[7] = new int2(-1, 1);

            int counter = 0;

            do
            {
                //get closest in searchable table
                current = searchSet[ClosestNode()];

                //add to the usable node map for iterating neighbours
                moveSet.TryAdd(current.coordinate, current);

                //iterate neighbours with movement offsets
                for (int i = 0; i < movementWay.Length; i++)
                {
                    if (!moveSet.ContainsKey(current.coordinate + movementWay[i]) && !obstacleSet.ContainsKey(current.coordinate + movementWay[i]))
                    {
                        AStarNode neighbourNode = new AStarNode
                        {
                            coordinate = current.coordinate + movementWay[i],
                            parent = current.coordinate,
                            gScore = current.gScore + GetPisagorDistance(current.coordinate, current.coordinate + movementWay[i]),
                            hScore = GetPisagorDistance(current.coordinate + movementWay[i], endingNode.coordinate)
                        };

                        if (searchSet.ContainsKey(neighbourNode.coordinate) && neighbourNode.gScore < searchSet[neighbourNode.coordinate].gScore)
                        {
                            //update old moveable data
                            searchSet[neighbourNode.coordinate] = neighbourNode;
                        }
                        else if (!searchSet.ContainsKey(neighbourNode.coordinate))
                        {
                            //add new moveable node
                            searchSet.TryAdd(neighbourNode.coordinate, neighbourNode);
                        }

                    }


                }

                //remove iterated usable node 
                searchSet.Remove(current.coordinate);
                counter++;


                //redline for infinite loops (max iteration count)
                if (counter > redLine)
                    break;


            } while (searchSet.Count() != 0);
        }


        /// <summary>
        /// Simply find nearest
        /// </summary>
        /// <returns></returns>
        public int2 ClosestNode()
        {
            AStarNode result = new AStarNode();
            int fScore = int.MaxValue;

            // temp alloc for  one frame stuff
            NativeArray<AStarNode> nodeArray = searchSet.GetValueArray(Allocator.Temp);

            for (int i = 0; i < nodeArray.Length; i++)
            {
                if (nodeArray[i].gScore + nodeArray[i].hScore < fScore)
                {
                    fScore = nodeArray[i].gScore + nodeArray[i].hScore;
                    result = nodeArray[i];
                }
            }

            nodeArray.Dispose();
            return result.coordinate;
        }

        /// <summary>
        /// Get distance with pisagor teory
        /// </summary>
        /// <param name="indexA">start location</param>
        /// <param name="IndexB">end location</param>
        /// <returns></returns>
        public int GetPisagorDistance(int2 indexA, int2 IndexB)
        {
            int a = IndexB.x - indexA.x;
            int b = IndexB.y - indexA.y;

            return a * a + b * b;
        }


    }
}

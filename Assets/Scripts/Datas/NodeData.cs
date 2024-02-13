namespace AgeOfKing.AStar
{
    public struct AStarNode
    {
        public Unity.Mathematics.int2 coordinate;
        public Unity.Mathematics.int2 parent;
        public int gScore;
        public int hScore;
    }
}

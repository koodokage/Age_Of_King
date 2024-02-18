using System.Collections.Generic;
using UnityEngine;

namespace AgeOfKing.Systems
{
    public interface IHighlighter
    {
        public void ShowTile(Dictionary<Vector3Int, bool> tiles);
        public void ShowPath(Vector3Int[] paths, int pathLength);
        public void Close();
    }

}
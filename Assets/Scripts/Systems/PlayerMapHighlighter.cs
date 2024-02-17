using System.Collections.Generic;
using UnityEngine;

namespace AgeOfKing.Systems
{
    public class PlayerMapHighlighter : IHighlighter
    {
        public void Close()
        {
            MapHighlighter.GetInstance.ClearAll();
        }

        public void ShowPath(Vector3Int[] paths, int estimatedLength)
        {
            MapHighlighter hgSystem = MapHighlighter.GetInstance;
            hgSystem.ClearAll();
            Vector3Int path;

            for (int i = paths.Length-1; i >= 0; i--)
            {
                path = paths[i];
                if (i < estimatedLength)
                {
                    hgSystem.Highlight(path, true);
                    continue;
                }
                hgSystem.Highlight(path, false);
            }

        }

        public void ShowTile(Dictionary<Vector3Int, bool> tiles)
        {
            MapHighlighter hgSystem = MapHighlighter.GetInstance;
            hgSystem.ClearAll();
            foreach (var tile in tiles.Keys)
            {
                bool state = tiles[tile];
                hgSystem.Highlight(tile, state);
            }
        }


    }

}
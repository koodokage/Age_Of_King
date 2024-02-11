using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Datas
{
    [RequireComponent(typeof(Tilemap))]
    public class TileMapData : MonoBehaviour
    {
        Tilemap _map;

        private void Awake()
        {
            _map = GetComponent<Tilemap>();
        }

        public IEnumerable<Vector3Int> GetAllPaintedTiles()
        {
            var bound = _map.cellBounds;
            for (int x = bound.min.x; x < bound.max.x; x++)
            {
                for (int y = bound.min.y; y < bound.max.y; y++)
                {
                    var cellPosition = new Vector3Int(x, y, 0);
                    var sprite = _map.GetSprite(cellPosition);
                    var tile = _map.GetTile(cellPosition);

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

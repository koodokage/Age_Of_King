using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapHighlighter : SingleBehaviour<MapHighlighter>
{
    [Header("Highlight")]
    [SerializeField] Tile tile;
    [SerializeField] Color blockedTile;
    [SerializeField] Color placeableTile;

    Tilemap _hgMap;
    bool isUsed;

    private void Start()
    {
        _hgMap = Maps.GetInstance.GetHighlightMap;
        isUsed = false;
    }

    public void ClearAll()
    {
        if(isUsed)
            _hgMap.ClearAllTiles();
    }

    public void Highlight(Vector3Int cellPosition,bool isBlocked)
    {
        _hgMap.SetTile(cellPosition, tile);
        _hgMap.SetTileFlags(cellPosition, TileFlags.None);

        Color currentColor = placeableTile;
        if (isBlocked)
        {
            currentColor = blockedTile;
        }

        _hgMap.SetColor(cellPosition, currentColor);

        isUsed = true;

    }


}

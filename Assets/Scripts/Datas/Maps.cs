using UnityEngine;
using UnityEngine.Tilemaps;

public class Maps : SingleBehaviour<Maps>
{
    [SerializeField] Tilemap groundMap;
    [SerializeField] Tilemap obstacleMap;
    [SerializeField] Tilemap highlightMap;
    [SerializeField] Tilemap buildingMap;
    [SerializeField] Tilemap unitMap;

    public Tilemap GetGroundMap { get => groundMap; }
    public Tilemap GetObstacleMap { get => obstacleMap; }
    public Tilemap GetHighlightMap { get => highlightMap; }
    public Tilemap GetBuildingMap { get => buildingMap; }
    public Tilemap GetUnitMap { get => unitMap; }

}

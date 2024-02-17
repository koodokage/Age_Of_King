using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Data
{
    /// <summary>
    /// game map layer datas 
    /// </summary>
    public class Map : ASingleBehaviour<Map>
    {

        [SerializeField] Tilemap groundMap;
        [SerializeField] Tilemap obstaclesMap;
        [SerializeField] Tilemap highlightMap;
        [SerializeField] Tilemap buildingsMap;
        [SerializeField] Tilemap unitsMap;

        public Tilemap GetGroundMap { get => groundMap; }
        public Tilemap GetObstacleMap { get => obstaclesMap; }
        public Tilemap GetHighlightMap { get => highlightMap; }
        public Tilemap GetBuildingMap { get => buildingsMap; }
        public Tilemap GetUnitMap { get => unitsMap; }


        private void Start()
        {
            MapEntityDataBase.Initiliaze();
        }



        


    }

}
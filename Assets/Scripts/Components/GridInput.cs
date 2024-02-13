using UnityEngine;
using AgeOfKing.Systems.Input;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;
using AgeOfKing.Datas;

namespace AgeOfKing.Components
{
    public class GridInput : MonoBehaviour
    {
        Camera _mainCamera;

        [SerializeField] Tilemap groundMap;
        public LayerMask SelectableLayer;

        private static Vector3Int _currentMapCell;
        public static Vector3Int GetCurrentMapCell { get => _currentMapCell;}


        private void Awake()
        {
            _mainCamera = Camera.main;
            Assert.IsNotNull(_mainCamera);
        }


        private void OnEnable()
        {
            //Listen inputs
            InputManager.OnMousePositionChanged += OnChange_MousePosition;
            InputManager.OnMouseLeftClicked += OnClicked_MouseLeft;
            InputManager.OnMouseRightClicked += OnClicked_MouseRight;
        }

        private void OnDisable()
        {
            //Release inputs
            InputManager.OnMousePositionChanged -= OnChange_MousePosition;
            InputManager.OnMouseLeftClicked -= OnClicked_MouseLeft;
            InputManager.OnMouseRightClicked -= OnClicked_MouseRight;
        }

        /// <summary>
        /// Move unit input trigger
        /// </summary>
        private void OnClicked_MouseRight()
        {

            //if (isMoving)
            //    return;

            //movementToggle = true;
            //targetLocation = symbol.transform.position;
        }

        /// <summary>
        /// Select unit input trigger
        /// </summary>
        void OnClicked_MouseLeft(Vector2 mouseLocation)
        {
            Vector3 mouseLoc = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mouseCell = groundMap.WorldToCell(mouseLoc);

            var tile = groundMap.GetTile(mouseCell);
            if (tile != null)
            {
               if(MapEntityData.GetInstance.IsPlaceBulding(mouseCell,out ABuilding building))
                {
                    Debug.LogWarning($"[SELECTED INSTANCE] {building.GetData.BuildingLabel}");
                }
            }
        }

        /// <summary>
        /// Transform pointer data to ground map cell location
        /// </summary>
        /// <param name="mouseLocation"></param>
        void OnChange_MousePosition(Vector2 mouseLocation)
        {
            Vector3 mouseLoc = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mouseCell = groundMap.WorldToCell(mouseLoc);
            var tile = groundMap.GetTile(mouseCell);
            if (tile != null)
            {
                _currentMapCell = mouseCell;
            }
        }

        //private void Update()
        //{
        //    //TEMPO CODE 

        //    if (selectedPawn == null)
        //        return;

        //    if (movementToggle == false)
        //        return;

        //    if ((targetLocation - selectedPawn.transform.position).magnitude < 0.1f)
        //    {
        //        selectedPawn.transform.position = targetLocation;
        //        movementToggle = false;
        //        selectedPawn = null;
        //        isMoving = false;
        //        return;
        //    }

        //    selectedPawn.transform.position = Vector3.MoveTowards(selectedPawn.transform.position, targetLocation, Time.deltaTime * 4f);
        //    isMoving = true;
        //}


    }

}

using UnityEngine;
using AgeOfKing.Systems.Input;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;
using AgeOfKing.Datas;
using AgeOfKing.Systems.UI;
using AgeOfKing.Abstract.Components;
using AgeOfKing.AStar;
using System.Collections.Generic;
using AgeOfKing.Systems;

namespace AgeOfKing.Components
{
    public class MapInput : MonoBehaviour
    {
        Camera _mainCamera;

        private static Vector3Int _currentSelectedMapCell;
        public static Vector3Int GetCurrentMapCell { get => _currentSelectedMapCell; }


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
        private void OnClicked_MouseRight(Vector2 mouseLocation)
        {
            if (TryGetTileByLocation(mouseLocation, out TileBase tile, out Vector3Int mouseCell))
            {
                ComandController.MoveAction();
            }

        }


        /// <summary>
        /// Select unit input trigger
        /// </summary>
        void OnClicked_MouseLeft(Vector2 mouseLocation)
        {
            if (TryGetTileByLocation(mouseLocation, out TileBase tile, out Vector3Int mouseCell))
            {
                if (MapEntityDataBase.GetInstance.IsTileContainSelecteable(mouseCell, out ISelectable selectable))
                {
                    selectable.OnSelected();


                }
                else
                {
                    UIManager.GetInstance.OnClickEmpty();
                }

                MapEntityDataBase.GetInstance.IsTileContainUnit(mouseCell, out AUnit unit);
                ComandController.SetControllableUnit(unit);
            }
        }

        /// <summary>
        /// Transform pointer data to ground map cell location
        /// </summary>
        /// <param name="mouseLocation"></param>
        void OnChange_MousePosition(Vector2 mouseLocation)
        {
            if (TryGetTileByLocation(mouseLocation, out TileBase tile, out Vector3Int mouseCell))
            {
                _currentSelectedMapCell = mouseCell;
            }

            ComandController.ShowPath(mouseCell);

        }

        private bool TryGetTileByLocation(Vector2 mouseLocation, out TileBase tileBase, out Vector3Int mouseCell)
        {
            Vector3 mouseLoc = _mainCamera.ScreenToWorldPoint(mouseLocation);
            mouseCell = Maps.GetInstance.GetGroundMap.WorldToCell(mouseLoc);

            tileBase = Maps.GetInstance.GetGroundMap.GetTile(mouseCell);

            if (tileBase == null)
                return false;

            return true;
        }


    }

}

using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.Systems.Input;
using AgeOfKing.Systems.UI;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Components
{


    public class PlayerMapInput:IMapInput
    {
        Camera _mainCamera;

        private static Vector3Int _currentSelectedMapCell;
        public static Vector3Int GetCurrentMapCell { get => _currentSelectedMapCell; }

        public IPlayer Owner { get; private set; }

        public event Action<Vector3Int> OnCellHover;
        public event Action<Vector3Int, ISelectable, AUnit> OnCellSelected;
        public event Action<Vector3Int, IHittable> OnCellUnitCommand;

        public PlayerMapInput(IInputManager manager,IPlayer player)
        {
            Owner = player;
            _mainCamera = Camera.main;
            Assert.IsNotNull(_mainCamera);
            BindInput(manager);
        }

        public void BindInput(IInputManager manager)
        {
            manager.OnPointerLocationChanged += CellHover;
            manager.OnPointerSelectionClicked += CellSelected;
            manager.OnPointerCommandClicked += CellUnitCommand;
        }

        private bool TryGetTileByLocation(Vector2 mouseLocation, out TileBase tileBase, out Vector3Int mouseCell)
        {
            Vector3 mouseLoc = _mainCamera.ScreenToWorldPoint(mouseLocation);
            mouseCell = Map.GetInstance.GetGroundMap.WorldToCell(mouseLoc);

            tileBase = Map.GetInstance.GetGroundMap.GetTile(mouseCell);

            if (tileBase == null)
                return false;

            return true;
        }

        /// <summary>
        /// Transform pointer data to ground map cell location
        /// </summary>
        /// <param name="pointerLocation"></param>
        public void CellHover(Vector2 pointerLocation)
        {
            if(TryGetTileByLocation(pointerLocation, out TileBase tile, out _currentSelectedMapCell))
            {
                OnCellHover?.Invoke(_currentSelectedMapCell);
            }
        }

        /// <summary>
        /// Select cell input trigger
        /// </summary>
        /// <param name="pointerLocation"></param>
        public void CellSelected(Vector2 pointerLocation)
        {
            if (TryGetTileByLocation(pointerLocation, out TileBase tile, out Vector3Int mouseCell))
            {
                AUnit unit = null;
                if (MapEntityDataBase.IsTileContainSelecteable(mouseCell, out ISelectable selectable))
                {
                    selectable.OnSelected();
                    if(MapEntityDataBase.IsTileContainUnit(mouseCell, out  AUnit selecteableUnit))
                    {
                        unit = selecteableUnit.GetOwnerPlayer == Owner ? selecteableUnit : null;
                    }
                }
                else
                {
                    UIManager.GetInstance.OnClickEmpty();
                }


                OnCellSelected?.Invoke(mouseCell, selectable, unit);
            }
        }

        /// <summary>
        /// Move unit input trigger
        /// </summary>
        /// <param name="pointerLocation"></param>
        public void CellUnitCommand(Vector2 pointerLocation)
        {
            Debug.Log($"COMMANDER PLAYER {Owner.Name}");
            if (TryGetTileByLocation(pointerLocation, out TileBase tile, out Vector3Int mouseCell))
            {
                IHittable hittable = null;
              
                if (MapEntityDataBase.IsTileContainUnit(mouseCell, out AUnit unit))
                {
                    if (unit.GetOwnerPlayer != Owner)
                    {
                        MapEntityDataBase.IsTileContainHittable(mouseCell, out hittable);
                    }

                }else if (MapEntityDataBase.IsTileContainExtrudedBuilding(mouseCell, out ABuilding building))
                {
                    if (building.GetOwnerPlayer != Owner)
                    {
                        Debug.Log($"BUILDING OWNER  {building.GetOwnerPlayer.Name}");
                        MapEntityDataBase.IsTileContainHittable(mouseCell, out hittable);
                    }
                }

                Debug.Log($"COMMAND SENDED");

                OnCellUnitCommand?.Invoke(mouseCell, hittable);
            }
        }

    }

}

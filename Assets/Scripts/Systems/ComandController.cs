using AgeOfKing.Abstract.Components;
using AgeOfKing.AStar;
using UnityEngine;

namespace AgeOfKing.Systems
{
    public static class ComandController
    {
        static AUnit _currentControlledUnit;
        static Vector3Int _currentMoveableCell;
        static bool _isPathCompleted;
        public static void SetControllableUnit(AUnit unit)
        {
            _currentControlledUnit = unit;
            if (unit == null)
            {
                MapHighlighter.GetInstance.ClearAll();
                return;
            }

        }

        public static void MoveAction()
        {
            if (_isPathCompleted == false || _currentControlledUnit ==null)
                return;

            _currentControlledUnit.MoveTo(_currentMoveableCell);
            MapHighlighter.GetInstance.ClearAll();
        }

        public static void ShowPath(Vector3Int targetCell)
        {
            if (_currentControlledUnit == null)
                return;

            _isPathCompleted = PathFinder.FindAndShowPath(_currentControlledUnit.GetCurrentCellLocation, targetCell, _currentControlledUnit.GetData.GetMovement, out _currentMoveableCell);
        }

    }

}
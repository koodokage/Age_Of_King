using AgeOfKing.Abstract.Components;
using AgeOfKing.AStar;
using AgeOfKing.Components;
using AgeOfKing.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AgeOfKing.Systems
{

    public class PlayerCommandController : ICommandController
    {
        AUnit _currentControlledUnit;
        Vector3Int _currentMoveableCell;
        int _calculatedMovementCost;
        bool _isPathValid;

        public IPlayer Owner { get; private set; }
        public event Action<Vector3Int[], int> OnPathCreated;
        public event Action OnCommandStateReset;

        public PlayerCommandController(IPlayer player)
        {
            Owner = player;
        }


        public void SetControllableUnit(Vector3Int cellLocation, ISelectable selectable, AUnit unit)
        {
            OnCommandStateReset?.Invoke();

            if (unit == null)
            {
                _currentControlledUnit = null;
                return;
            }

            if (unit.GetOwnerPlayer == Owner)
            {
                _currentControlledUnit = unit;
            }
            else
            {
                _currentControlledUnit = null;
            }

        }

        public void UnitCommand(Vector3Int cell, IHittable hittableUnit)
        {
            Debug.Log($"COMMAND {Owner.Name}");


            if (_currentControlledUnit == null)
                return;

            AttackCommand(cell, hittableUnit);
            MoveCommand();

            OnCommandStateReset?.Invoke();
        }

        public void MoveCommand()
        {
            if (_currentControlledUnit.CurrentMovePoint == 0)
                return;

            if (_isPathValid == false)
                return;

            _currentControlledUnit.MoveTo(_currentMoveableCell, _calculatedMovementCost);
        }

        public void BuildCommand(BuildingData data, IPlayer player)
        {
            ABuilding buildingInstance = BuildingFactory.GetInstance.Produce(data, player);
            Vector3Int mouseCell = PlayerMapInput.GetCurrentMapCell;
            buildingInstance.Draw(mouseCell);
        }

        public bool AttackCommand(Vector3Int cell, IHittable hittable)
        {
            bool attackSuccess = false;

            if (hittable != null)
            {

                if (_currentControlledUnit.TryGetComponent(out ISoldier soldier))
                {
                    attackSuccess = soldier.IsInAttackRange(cell);
                    if (attackSuccess)
                    {
                        soldier.Attack(hittable);

                    }
                }
            }

            return attackSuccess;
        }


        public void CalculatePath(Vector3Int targetCell)
        {
            if (_currentControlledUnit == null)
                return;

            if (_currentControlledUnit.CurrentMovePoint <= 0)
                return;

            _isPathValid = PathFinder.GetPath(_currentControlledUnit.GetCurrentCellLocation, targetCell,
                _currentControlledUnit.CurrentMovePoint, out Vector3Int[] paths, out _currentMoveableCell, out _calculatedMovementCost);

            if (_isPathValid)
            {
                _calculatedMovementCost = Mathf.Clamp(_currentControlledUnit.CurrentMovePoint, 0, paths.Length);
                int estimated = paths.Length - _calculatedMovementCost;
                _currentMoveableCell = paths[estimated];
                OnPathCreated?.Invoke(paths, estimated);
            }
        }

    }

}
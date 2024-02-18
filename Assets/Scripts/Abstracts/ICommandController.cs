using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
using System;
using UnityEngine;

namespace AgeOfKing.Systems
{

    public interface  ICommandController
    {
        public enum CommandResult { MOVE,ATTACK,HIT,SPAWN,DIE,DESTROY}

        public  IPlayer Owner { get;}
        public  bool IsCurrentUnitValid { get; }
        public event Action<Vector3Int[], int> OnPathCreated;
        public event Action OnCommandStateReset;

        public void SetControllableUnit(Vector3Int cell, ISelectable selectable, AUnit unit);
        public void UnitCommand(Vector3Int cell, IHittable hittable);
        public void MoveCommand();
        public bool AttackCommand(Vector3Int cell,IHittable hittable);
        public void BuildCommand(BuildingData data, IPlayer player);
        public void CalculatePath(Vector3Int targetCell);
        public void Release();


    }

}
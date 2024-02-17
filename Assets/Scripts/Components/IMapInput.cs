using AgeOfKing.Abstract.Components;
using AgeOfKing.Systems;
using AgeOfKing.Systems.Input;
using System;
using UnityEngine;

namespace AgeOfKing.Components
{
    public interface IMapInput
    {
        public IPlayer Owner { get; }
        public event Action<Vector3Int> OnCellHover;
        public event Action<Vector3Int, ISelectable, AUnit> OnCellSelected;
        public event Action<Vector3Int,IHittable> OnCellUnitCommand;

        public void CellHover(Vector2 pointerLocation);
        public void CellSelected(Vector2 pointerLocation);
        public void CellUnitCommand(Vector2 pointerLocation);

        public void BindInput(IInputManager manager);
    }

}

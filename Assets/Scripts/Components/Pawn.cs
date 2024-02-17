using AgeOfKing.Abstract.Components;
using AgeOfKing.Systems;
using AgeOfKing.Systems.UI;
using UnityEngine;

namespace AgeOfKing.Components
{
    /// <summary>
    ///  moveable and selectable unit
    /// </summary>
    public class Pawn : Character, ISelectable
    {
        public void OnSelected()
        {
            if (owner == TurnManager.GetInstance.GetTurnPlayer)
            {
                UIManager.GetInstance.OnUnitSelected(this);
            }
        }

        public override void MoveTo(UnityEngine.Vector3Int cellLocation, int movementCost = 0)
        {
            base.MoveTo(cellLocation, movementCost);
            //UpdateUI
            OnSelected();
        }
    }

}

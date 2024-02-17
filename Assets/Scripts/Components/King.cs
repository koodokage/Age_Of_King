﻿using AgeOfKing.Abstract.Components;
using AgeOfKing.Systems;
using AgeOfKing.Systems.UI;

namespace AgeOfKing.Components
{
    /// <summary>
    ///  kingdom king with move block
    /// </summary>
    public class King : Character, ISelectable
    {
        public void OnSelected()
        {
            if (owner == TurnManager.GetInstance.GetTurnPlayer)
                UIManager.GetInstance.OnUnitSelected(this);
        }

        public override void MoveTo(UnityEngine.Vector3Int cellLocation, int movementCost = 0)
        {
            base.MoveTo(cellLocation, movementCost);
            owner.GetVillage.UseKingMoveRights(1);

            if (owner.GetVillage.IsKingMoveRightsEnough(1) == false)
            {
                _currentMovePoint = 0;
            }

            //UpdateUI
            OnSelected();
        }

        public override void OnTurnChange(IPlayer side, int turnIndex)
        {
            if (owner.GetVillage.IsKingMoveRightsEnough(1))
            {
                base.OnTurnChange(side, turnIndex);
            }
        }

    }

}
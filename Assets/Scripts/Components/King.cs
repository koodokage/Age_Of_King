using AgeOfKing.Systems;
using UnityEngine;

namespace AgeOfKing.Components
{
    /// <summary>
    ///  kingdom king with move block
    /// </summary>
    public class King : Pawn
    {
        public override void MoveTo(UnityEngine.Vector3Int cellLocation, int movementCost = 0)
        {
            base.MoveTo(cellLocation, movementCost);
            owner.GetVillage.UseKingMoveRights(1);

            if (owner.GetVillage.IsKingMoveRightsEnough(1) == false)
            {
                _currentMovePoint = 0;
            }

        }

        public override void OnTurnChange(IPlayer side, int turnIndex)
        {
            if (owner.GetVillage.IsKingMoveRightsEnough(1))
            {
                base.OnTurnChange(side, turnIndex);
            }
        }

        public override void Erase()
        {
            GameManager.GetInstance.OnKingDies(owner);
        }
    }

}

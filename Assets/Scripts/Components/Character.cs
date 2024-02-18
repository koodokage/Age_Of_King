using AgeOfKing.Abstract.Components;
using AgeOfKing.AStar;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Components
{
    public class Character : AUnit, IHittable, ISoldier
    {
        protected int _attackRights;
        public int AttackRights { get => _attackRights; }


        public override void InitializeData(UnitData data, IPlayer player)
        {
            base.InitializeData(data, player);

            if (data.TryGetValueByGenre(Data.CharacterStatGenre.ATTACKRIGHTS, out _attackRights) == false)
                Debug.LogError($"THERE IS NO {Data.CharacterStatGenre.ATTACKRIGHTS} IN CHARACTER");

            if (data.TryGetValueByGenre(Data.CharacterStatGenre.MOVEMENT, out _currentMovePoint) == false)
                Debug.LogError($"THERE IS NO {Data.CharacterStatGenre.MOVEMENT} IN CHARACTER");
        }

        public override void OnTurnChange(IPlayer side, int turnIndex)
        {
            if (owner == side)
            {
                GetData.TryGetValueByGenre(Data.CharacterStatGenre.ATTACKRIGHTS, out _attackRights);
                GetData.TryGetValueByGenre(CharacterStatGenre.MOVEMENT, out _currentMovePoint);
            }

        }


        public void Attack(IHittable hittable)
        {
            if (_attackRights > 0)
            {
                if (GetData.TryGetValueByGenre(Data.CharacterStatGenre.ATTACK, out int damage) == false)
                    Debug.LogError($"THERE IS NO {Data.CharacterStatGenre.ATTACK} IN CHARACTER");

                hittable.Hit(damage);
                _attackRights--;
            }

            if (_attackRights <= 0)
            {
                _currentMovePoint = 0;
            }

        }

        public bool Hit(int damage)
        {
            bool isDead = false;

            currentHealth -= damage;

            isDead = currentHealth <= 0 ? true : false;

            Action onEnd = null;
            if (currentHealth <= 0)
            {
                onEnd = Erase;
            }

            StartCoroutine(FlickerAnimation(Map.GetInstance.GetUnitMap,Color.red,currentCellLocation,onEnd));
            
            return isDead;
        }

        

        public bool IsInAttackRange(Vector3Int targetLocation)
        {
            if (GetData.TryGetValueByGenre(CharacterStatGenre.ATTACKRANGE, out int range))
            {
                Vector3Int offset;
                foreach (var location in MapUtils.MovementWays)
                {
                    offset = new Vector3Int(location.x * range, location.y * range, 0);
                    var attackRange = currentCellLocation + offset;
                    if (attackRange == targetLocation)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

}

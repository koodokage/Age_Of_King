using AgeOfKing.Abstract.Components;
using AgeOfKing.AStar;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.Utils;
using UnityEngine;

namespace AgeOfKing.Components
{
    public class Character : AUnit, IHittable, ISoldier
    {
        protected int currentHealth;
        public int CurrentHealth { get => currentHealth; }

        public override void InitializeData(UnitData data,IPlayer player)
        {
            base.InitializeData(data, player);

            if (data.TryGetValueByGenre(Data.CharacterStatGenre.HEALTH, out currentHealth) == false)
                Debug.LogError($"THERE IS NO {Data.CharacterStatGenre.HEALTH} IN CHARACTER");

            if (data.TryGetValueByGenre(Data.CharacterStatGenre.MOVEMENT, out _currentMovePoint) == false)
                Debug.LogError($"THERE IS NO {Data.CharacterStatGenre.MOVEMENT} IN CHARACTER");
        }

        public void Attack(IHittable hittable)
        {
            if (GetData.TryGetValueByGenre(Data.CharacterStatGenre.ATTACK, out int damage) == false)
                Debug.LogError($"THERE IS NO {Data.CharacterStatGenre.ATTACK} IN CHARACTER");

            hittable.Hit(damage);
        }

        public bool Hit(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                //particle
                //dead close
                return true;
            }

            //anim
            return false;
        }

        public bool IsInAttackRange(Vector3Int targetLocation)
        {
            if (GetData.TryGetValueByGenre(CharacterStatGenre.ATTACKRANGE, out int range))
            {
                Vector3Int offset;
                foreach (var location in MapUtils.MovementWays)
                {
                    offset = new Vector3Int(location.x * range, location.y * range, 0);
                    var attackRange = _currentCellLocation + offset;
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

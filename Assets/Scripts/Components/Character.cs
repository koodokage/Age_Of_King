using AgeOfKing.Abstract.Components;
using AgeOfKing.Datas;
using UnityEngine;

namespace AgeOfKing.Components
{
    public class Character : AUnit,ICharacter, IHittable, ISoldier
    {
        protected int _currentHealth;
        public int CurrentHealth { get => _currentHealth; }

        public override void InitializeData(UnitData data)
        {
            base.InitializeData(data);
            _currentHealth = data.GetHealth;
        }

        public virtual void Move(Vector3Int targetCellLocation)
        {
            OnTileExit();
            MoveTo(targetCellLocation);
        }

        public void Attack(IHittable hittable)
        {
            hittable.Hit(_unitData.GetAttack);
        }

        public bool Hit(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                //particle
                //dead close
                return true;
            }

            //anim
            return false;
        }

    }

}

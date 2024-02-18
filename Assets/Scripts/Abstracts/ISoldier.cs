using UnityEngine;

namespace AgeOfKing.Abstract.Components
{
    public interface ISoldier
    {
        public int AttackRights { get; }
        public void Attack(IHittable hit);
        public bool IsInAttackRange(Vector3Int targetLocation);
    }

}

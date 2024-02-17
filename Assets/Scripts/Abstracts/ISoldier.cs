using UnityEngine;

namespace AgeOfKing.Abstract.Components
{
    public interface ISoldier
    {
        public void Attack(IHittable hit);
        public bool IsInAttackRange(Vector3Int targetLocation);
    }

}

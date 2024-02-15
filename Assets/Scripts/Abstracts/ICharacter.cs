using UnityEngine;

namespace AgeOfKing.Abstract.Components
{
    public interface ICharacter
    {
        public void Move(Vector3Int targetCellLocation);
    }

}

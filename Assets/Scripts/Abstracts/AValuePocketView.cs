using UnityEngine;

namespace AgeOfKing.UI
{
    public abstract class AValuePocketView : MonoBehaviour
    {
        public abstract void Increased(int updated,int change);
        public abstract void Decreased(int updated,int change);
    }

}

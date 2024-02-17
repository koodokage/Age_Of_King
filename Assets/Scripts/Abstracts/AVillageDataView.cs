using AgeOfKing.Data;
using UnityEngine;

namespace AgeOfKing.UI
{
    public abstract class AVillageDataView : MonoBehaviour
    {
        public abstract void VillageDataChanged(VillageData updated);
    }

}

using AgeOfKing.Abstract.Components;
using AgeOfKing.Systems.UI;

namespace AgeOfKing.Components
{
    public class Warrior : Character, ISelectable
    {
        public void OnSelected()
        {
            UIManager.GetInstance.OnUnitSelected(this);
        }
    }

}

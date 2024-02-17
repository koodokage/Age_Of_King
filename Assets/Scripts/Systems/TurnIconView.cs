using UnityEngine;

namespace AgeOfKing.UI
{
    public class TurnIconView : MonoBehaviour
    {
        [SerializeField] GameObject turnOn;
        [SerializeField] GameObject turnOff;

        public void SetView(bool state)
        {
            turnOn.SetActive(state);
            turnOff.SetActive(!state);
        }

        
    }


}

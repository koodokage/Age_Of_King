using AgeOfKing.Abstract.Components;
using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMP_tip;

    public void GetTooltip(Vector2 cursorLocation,IHittable hittable)
    {
        TMP_tip.text = "Health: " + hittable.CurrentHealth;
        gameObject.SetActive(true);
        transform.position = cursorLocation;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
  
}

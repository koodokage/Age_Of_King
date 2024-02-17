using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public string CursorLabel_arrow;
    public string CursorLabel_move;
    public string CursorLabel_attack;
    [SerializeField] Image cursor;
    [SerializeField] Vector3 cursorOffset;

    private void Start()
    {
        SetImage(CursorLabel_arrow);
    }

    public void SetImage(string cursorImageLabel)
    {
        cursor.sprite = (Resources.Load("UIAtlas") as UnityEngine.U2D.SpriteAtlas).GetSprite(cursorImageLabel);
    }

    // Update is called once per frame
    void Update()
    {
        cursor.rectTransform.transform.position = Input.mousePosition + cursorOffset;
        if (Input.GetMouseButtonDown(0))
        {
            SetImage(CursorLabel_move);
        }
    }
}

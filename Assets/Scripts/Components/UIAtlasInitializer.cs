using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace AgeOfKing.UI
{
    public class UIAtlasInitializer : MonoBehaviour
    {
        public SpriteAtlas SpriteAtlas;
        public string SpriteLabel;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Image>().sprite = SpriteAtlas.GetSprite(SpriteLabel);
        }


    }
}

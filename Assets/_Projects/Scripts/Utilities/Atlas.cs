using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Utilities
{
    public class Atlas : MonoBehaviour
    {
        [SerializeField] private Image image;
        public SpriteAtlas spriteAtlas;
        public string spriteName;

        private void Awake()
        {
            image.sprite = null;
            if (!string.IsNullOrEmpty(spriteName))
            {
                image.sprite = spriteAtlas.GetSprite(spriteName);
            }
        }

        public void SetSprite(string spriteName)
        {
            this.spriteName = spriteName;
            Sprite sprite = spriteAtlas.GetSprite(spriteName);
            sprite.name = sprite.name.Replace("(Clone)", string.Empty);
            image.sprite = sprite;
        }
#if UNITY_EDITOR
    public void SetSpriteOnEditMode(Sprite sprite)
    {
        this.spriteName = sprite.name;
        image.sprite = sprite;
    }
    void OnValidate()
    {
        if (image == null)
        {
            image = this.GetComponent<Image>();
        }
    }
#endif
    }
}

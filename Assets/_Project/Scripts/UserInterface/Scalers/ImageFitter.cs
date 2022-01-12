using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Scalers
{
    public class ImageFitter : MonoBehaviour
    {
        [SerializeField] private RectTransform imageArea;
        [SerializeField] private RectTransform imageRect;
        [SerializeField] private RawImage image;
        private void Awake()
        {
            if (image.texture)
            {
                InitializeImage(image.texture);
            }
        }

        public void InitializeImage(Texture texture)
        {
            StartCoroutine(SetTextureSize(texture));
        }

        private IEnumerator SetTextureSize(Texture texture)
        {
            yield return new WaitForEndOfFrame();

            Vector2 size = TextureScaler.GetScaledTexture(imageArea, texture);
            image.texture = texture;
            imageRect.sizeDelta = new Vector2(size.x, size.y);
            Debug.Log(imageRect.sizeDelta);
        }
    }
}
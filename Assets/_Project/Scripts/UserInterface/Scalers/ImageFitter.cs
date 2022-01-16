using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Scalers
{
    public class ImageFitter : MonoBehaviour
    {
        [FormerlySerializedAs("imageArea")] [SerializeField] private RectTransform _imageArea;
        [FormerlySerializedAs("imageRect")] [SerializeField] private RectTransform _imageRect;
        [FormerlySerializedAs("image")] [SerializeField] private RawImage _image;
        
        private void Start()
        {
            if (_image.texture)
            {
                InitializeImage(_image.texture);
            }
        }
        public void InitializeImage(Texture texture)
        {
            StartCoroutine(SetTextureSize(texture));
        }

        private IEnumerator SetTextureSize(Texture texture)
        {
            yield return new WaitForEndOfFrame();

            Debug.Log(_imageArea.sizeDelta);
            Vector2 size = TextureScaler.GetScaledTexture(_imageArea, texture);
            _image.texture = texture;
            _imageRect.sizeDelta = new Vector2(size.x, size.y);
            Debug.Log(_imageRect.sizeDelta);
        }
    }
}
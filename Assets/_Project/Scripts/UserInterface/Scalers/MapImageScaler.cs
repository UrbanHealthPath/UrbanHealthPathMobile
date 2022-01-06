using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Scalers
{
    [RequireComponent(typeof(RectTransform), typeof(RawImage))]
    public class MapImageScaler : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private RawImage _image;
        private float _width = 1;
        private float _height = 1;
        private float _x = 0;
        private float _y = 0;
        private float _rectWidth;
        private float _rectHeight;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<RawImage>();
            _rectHeight = _rectTransform.rect.height;
            _rectWidth = _rectTransform.rect.width;
        }

        private void Start()
        {
            CalculateImageRect();
            _image.uvRect = new Rect(new Vector2(_x, _y), new Vector2(_width, _height));
        }

        private void CalculateImageOffset(float ratio, float paramFirst, float paramSecond)
        {
            float diff = paramFirst - paramSecond;
            float result = diff / (paramFirst * 2);

            if (ratio < 1)
            {
                _x = result;
                _y = 0;
            }
            else
            {
                _x = 0;
                _y = result;
            }
        }

        private void CalculateImageRect()
        {
            float ratio = _rectWidth / _rectHeight;

            if (!Mathf.Approximately(ratio, 0))
            {
                float paramFirst = 0, paramSecond = 0;
                if (ratio < 1)
                {
                    _width = ratio;
                    _height = 1;
                    paramFirst = _rectHeight;
                    paramSecond = _rectWidth;
                }
                else
                {
                    _width = 1;
                    _height = _rectHeight / _rectWidth;
                    paramFirst = _rectWidth;
                    paramSecond = _rectHeight;
                }

                CalculateImageOffset(ratio, paramFirst, paramSecond);
            }
        }
    }
}
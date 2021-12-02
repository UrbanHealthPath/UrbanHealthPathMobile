using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public interface IPopup
    {
        public RectTransform PopupArea { get;  }
        public void Display();

        public void Initialize();

        public void Close();

        public void InitSizeAndPosition(PopupPayload payload);
    }
}
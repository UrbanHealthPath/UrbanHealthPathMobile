using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public interface IPopup : IDisplayable
    {
        public RectTransform PopupArea { get;  }

        public void InitSizeAndPosition(PopupPayload payload);
    }
}
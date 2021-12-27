using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Interfaces
{
    public interface IPopup
    {
        public RectTransform PopupArea { get;  }

        public void InitSizeAndPosition(PopupPayload payload);
    }
}
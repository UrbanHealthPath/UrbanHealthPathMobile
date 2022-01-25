using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Interfaces
{
    public interface IInitializableView
    {
        public void Initialize(IViewInitializationParameters initializationParameters);
    }
}

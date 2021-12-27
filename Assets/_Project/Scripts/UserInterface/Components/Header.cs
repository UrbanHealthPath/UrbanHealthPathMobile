using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    public class Header : MonoBehaviour, IInitializable
    {
        [SerializeField] private TextMeshProUGUI text;
        
        public void Initialize(Initializer initializer)
        {
            if (initializer is HeaderInitializer init)
            {
                text.text = init.Text;
            }
        }
    }
}

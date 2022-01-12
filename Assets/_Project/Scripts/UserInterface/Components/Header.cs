using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    public class Header : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        public void Initialize(string headerText)
        {
            text.text = headerText;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public struct QuizElementOption 
    {
        public Texture2D Texture { get; }
        
        public UnityAction ButtonTextureAction { get; }

        public QuizElementOption(Texture2D texture, UnityAction buttonTextureAction)
        {
            Texture = texture;
            ButtonTextureAction = buttonTextureAction;
        }
    }
}

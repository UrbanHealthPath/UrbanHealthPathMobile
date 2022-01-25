using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public struct QuizElementOption 
    {
        public Texture2D Texture { get; }
        
        public UnityAction<QuizOptionButton> ButtonTextureAction { get; }

        public QuizElementOption(Texture2D texture, UnityAction<QuizOptionButton> buttonTextureAction)
        {
            Texture = texture;
            ButtonTextureAction = buttonTextureAction;
        }
    }
}

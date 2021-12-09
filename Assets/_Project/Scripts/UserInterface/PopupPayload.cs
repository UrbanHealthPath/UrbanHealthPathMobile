using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public class PopupPayload
    {
        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }

        public PopupPayload(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
            
            
        }
    }
}
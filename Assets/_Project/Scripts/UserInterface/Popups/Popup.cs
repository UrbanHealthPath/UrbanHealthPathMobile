using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public abstract class Popup : MonoBehaviour
    {
        public virtual void Display()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        public virtual void StopDisplay()
        {
            this.gameObject.SetActive(false);
        }
    }
}
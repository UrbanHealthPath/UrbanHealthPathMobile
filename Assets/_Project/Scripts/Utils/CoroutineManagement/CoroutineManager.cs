using System.Collections;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.CoroutineManagement
{
    public class CoroutineManager : MonoBehaviour, ICoroutineManager
    {
        public void BeginCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }

        public void EndCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}

using System.Collections;

namespace PolSl.UrbanHealthPath.Utils.CoroutineManager
{
    public interface ICoroutineManager
    {
        void BeginCoroutine(IEnumerator coroutine);
        void EndCoroutine(IEnumerator coroutine);
    }
}
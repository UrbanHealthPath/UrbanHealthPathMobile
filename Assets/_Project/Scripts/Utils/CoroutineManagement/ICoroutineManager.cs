using System.Collections;

namespace PolSl.UrbanHealthPath.Utils.CoroutineManagement
{
    public interface ICoroutineManager
    {
        void BeginCoroutine(IEnumerator coroutine);
        void EndCoroutine(IEnumerator coroutine);
    }
}
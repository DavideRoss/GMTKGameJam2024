using UnityEngine;
using System.Collections;

namespace Kickass
{
    using Action = System.Action;

    public static class LambdaInvoke
    {
        public static void Invoke(this MonoBehaviour mb, Action f, float delay)
        {
            mb.StartCoroutine(InvokeRoutine(f, delay));
        }

        private static IEnumerator InvokeRoutine(Action f, float delay)
        {
            yield return new WaitForSeconds(delay);
            f();
        }
    }
}

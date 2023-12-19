using System;
using System.Collections;
using UnityEngine;

namespace CMCore.Utilities
{
    public static class CoroutineHelper
    {
        public static void DelayedAction(this MonoBehaviour mono, Action action, float delay, bool realtime = false)
        {
            mono.StartCoroutine(DelayedActionRoutine(action, delay, realtime));
        }

        public static void DelayedAction(this MonoBehaviour mono, Action action, WaitUntil waitUntil)
        {
            mono.StartCoroutine(DelayedActionRoutine(action, waitUntil));
        }

        private static IEnumerator DelayedActionRoutine(Action action, float delay, bool realtime)
        {
            if (realtime)
            {
                yield return new WaitForSecondsRealtime(delay);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }

            action?.Invoke();
        }

        private static IEnumerator DelayedActionRoutine(Action action, WaitUntil waitUntil)
        {
            yield return waitUntil;
            action?.Invoke();
        }
    }
}
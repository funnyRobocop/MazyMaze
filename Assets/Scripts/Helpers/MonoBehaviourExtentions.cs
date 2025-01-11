using UnityEngine;
using System;
using System.Collections;

public static class MonoBehaviourExtentions
{

    public static void Invoke(this MonoBehaviour monoBehaviour, float time, Action action)
    {
        monoBehaviour.StartCoroutine(InvokeCoroutine(time, action));
    }

    private static IEnumerator InvokeCoroutine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    public static void InvokeAfterWaitFrame(this MonoBehaviour monoBehaviour, int frameCount, Action action)
    {
        monoBehaviour.StartCoroutine(InvokeCoroutine(frameCount, action));
    }

    private static IEnumerator InvokeAfterWaitFrameCoroutine(int frameCount, Action action)
    {
        for (int i = 0; i < frameCount; i++)
            yield return new WaitForEndOfFrame();

        action();
    }

    public static void InvokeRepeating(this MonoBehaviour monoBehaviour, Func<float> repeatTime, Action action)
    {
        monoBehaviour.StartCoroutine(InvokeRepeatingCoroutine(monoBehaviour, repeatTime, action));
    }

    private static IEnumerator InvokeRepeatingCoroutine(this MonoBehaviour monoBehaviour, Func<float> repeatTime, Action action)
    {
        yield return new WaitForSeconds(repeatTime());

        action();
        monoBehaviour.StartCoroutine(InvokeRepeatingCoroutine(monoBehaviour, repeatTime, action));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    Coroutine coroutine;
    IEnumerator timeRestore;

    float fixedDeltaTime;
    private void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }
    public void SlowdownTime(float scaleFactor)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        
        Time.timeScale = Mathf.Clamp(scaleFactor, 0, 1);
        //Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void RestoreRealTime(float timeToRestore)
    {
        //timeRestore = TimeRestore(timeToRestore);
        coroutine = StartCoroutine(TimeRestore(timeToRestore));
    }

    private IEnumerator TimeRestore(float timeToRestore)
    {
        float progress = 0f;
        float currentTime = 0f;
        float currentScale = Time.timeScale;

        while (progress < 1)
        {
            currentTime += Time.unscaledDeltaTime;
            progress = currentTime / timeToRestore;

            Time.timeScale = Mathf.Lerp(currentScale, 1, progress);

            yield return null;
        }
        Time.timeScale = 1f;
        //Time.fixedDeltaTime = fixedDeltaTime;
    }
}

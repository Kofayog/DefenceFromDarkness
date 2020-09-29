using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BloodSphereAnimationBehaviour : PlayableBehaviour
{
    public float startValue;
    public float endValue;

    private bool firstFrameHappend;
    private float distortion;
    private float defaultValue;

    private float duration;
    private float currentTime;
    private float progress;

    Material material;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);

        duration = (float)playable.GetDuration();

        currentTime = 0f;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        material = playerData as Material;

        if (material == null)
            return;

        if (!firstFrameHappend)
        {
            defaultValue = material.GetFloat("_Distortion");
            firstFrameHappend = true;
        }
        currentTime = (float)playable.GetTime();


        progress = currentTime / duration;

        distortion = Mathf.Lerp(startValue, endValue, progress);
        material.SetFloat("_Distortion", distortion);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        firstFrameHappend = false;

        if (material == null)
            return;

        material.SetFloat("_Distortion", defaultValue);
        base.OnBehaviourPause(playable, info);
    }
}

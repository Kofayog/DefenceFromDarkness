using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class AltarAnimationBehaviour : PlayableBehaviour
{
    public float duration;
    public Vector3 startPos;
    public Vector3 endPos;

    private bool firstFrameHappend;
    private Vector3 defaultPosition;

    private float currentTime;
    private float progress;

    private Transform transform;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);

        duration = (float) playable.GetDuration();

        currentTime = 0f;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        
        transform = playerData as Transform;

        if (!firstFrameHappend)
        {
            defaultPosition = transform.position;
            firstFrameHappend = true;
        }

        currentTime = (float) playable.GetTime();

        
        progress = currentTime / duration;

        transform.localPosition = Vector3.Lerp(startPos, endPos, progress);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        firstFrameHappend = false;

        if (transform == null)
            return;

        transform.position = defaultPosition;

        base.OnBehaviourPause(playable, info);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CanvasControlBehaviour : PlayableBehaviour
{
    public bool enabled;

    private bool firstFrameHappend;

    private Canvas canvas;

    private bool defaultEnabled;


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        canvas = playerData as Canvas;

        if (canvas == null)
            return;

        if (!firstFrameHappend)
        {
            defaultEnabled = canvas.enabled;
            firstFrameHappend = true;
        }

        canvas.enabled = enabled;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        firstFrameHappend = false;

        if (canvas == null)
            return;

        canvas.enabled = defaultEnabled;

        base.OnBehaviourPause(playable, info);

    }
}

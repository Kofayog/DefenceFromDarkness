using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CanvasControlClip : PlayableAsset
{
    public bool enabled;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CanvasControlBehaviour>.Create(graph);

        CanvasControlBehaviour canvasControlBehaviour = playable.GetBehaviour();
        canvasControlBehaviour.enabled = enabled;
        

        return playable;
    }
}

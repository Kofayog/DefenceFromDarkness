using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AltarAnimationClip : PlayableAsset
{
    public Vector3 startPos;
    public Vector3 endPos;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AltarAnimationBehaviour>.Create(graph);

        AltarAnimationBehaviour altarBehaviour = playable.GetBehaviour();

        altarBehaviour.startPos = startPos;
        altarBehaviour.endPos = endPos;

        return playable;
    }


}

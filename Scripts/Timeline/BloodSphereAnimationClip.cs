using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BloodSphereAnimationClip : PlayableAsset
{
    [Range(0, 1)] public float startValue;
    [Range(0, 1)] public float endValue;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<BloodSphereAnimationBehaviour>.Create(graph);

        BloodSphereAnimationBehaviour bloodSphereBehaviour = playable.GetBehaviour();

        bloodSphereBehaviour.startValue = startValue;
        bloodSphereBehaviour.endValue = endValue;

        return playable;
    }


}

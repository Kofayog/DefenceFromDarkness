using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CannonBallAbility : ScriptableObject
{
    public float cost;

    public abstract void Cast(CannonBallControl caster);
}

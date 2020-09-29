using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public SkillBehaviour skillBehaviour;

    public abstract void Cast(Transform origin, GameObject target);
}
